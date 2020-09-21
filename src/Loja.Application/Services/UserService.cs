using AutoMapper;
using Loja.Application.Templates;
using Loja.Application.Validators;
using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using Loja.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Loja.Application.Services
{
    public class UserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;        
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(SignInManager<User> signInManager,
                                   UserManager<User> userManager,
                                   IEmailService emailService,
                                   IConfiguration configuration,
                                   IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _mapper = mapper;
        }
       
        public async Task<UserDto> Login(AuthDto authDto)
        {
            var success = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Senha, false, false);

            if (!success.Succeeded)
                return null;

            var applicationUser = await GetUserByEmail(authDto.Email);
            var role = await GetUserRole(authDto.Email);
            var claims = await GetUserClaims(authDto.Email);
            
            return await Task.FromResult(new UserDto
            {
                Id = applicationUser.Id,
                Nome = applicationUser.Nome,
                Email = applicationUser.Email,
                Role = role,
                Claims = claims
            });
        }

        public async Task<ResultDto<UserDto>> SalvarCliente(UserDto userDto)
        {
            userDto.Role = "Cliente";
            var result = await Salvar(userDto);

            if(result.StatusCode == 200)
                _emailService.Send(userDto.Email, "Confirmação de cadastro", EmailTemplate.ConfirmacaoCadastro(_configuration, userDto));
            
            return await Task.FromResult(result);
        }

        public async Task<ResultDto<UserDto>> Salvar(UserDto userDto)
        {
            var userDtoValidate = new UserDtoValidate(userDto);
            if (!userDtoValidate.Validate())
                return ResultDto<UserDto>.Validation(userDtoValidate.Mensagens);

            var usuarioJaCadastrado = await _userManager.Users.FirstOrDefaultAsync(c => c.Email == userDto.Email);            

            if (usuarioJaCadastrado != null && usuarioJaCadastrado.IsGoogle)
                return ResultDto<UserDto>.Validation("Email já cadastrado via google!");
            else if (usuarioJaCadastrado != null && usuarioJaCadastrado.IsFacebook)
                return ResultDto<UserDto>.Validation("Email já cadastrado via facebook!");
            else if(usuarioJaCadastrado != null)
                return ResultDto<UserDto>.Validation("Email já cadastrado!");

            var user = new User
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                Nome = userDto.Nome,                
                IsFacebook = userDto.IsFacebook,
                IsGoogle = userDto.IsGoogle,
                DataCadastro = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, userDto.Senha);
            userDto.Id = user.Id;

            if (result.Succeeded && !string.IsNullOrWhiteSpace(userDto.Role))
                await AddUserRole(userDto);

            return await Task.FromResult(ResultDto<UserDto>.Success(userDto));
        }

        //public async Task<UserDto> Update(UserDto userDto)
        //{
        //    var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == userDto.Id);

        //    user.Nome = userDto.Nome;

        //    await _userManager.UpdateAsync(user);

        //    if (!string.IsNullOrEmpty(userDto.Senha))
        //    {
        //        await _userManager.RemovePasswordAsync(user);
        //        await _userManager.AddPasswordAsync(user, userDto.Senha);
        //    }

        //    await RemoveAndAddUserRoles(userDto);

        //    return await Task.FromResult(userDto);
        //}

        //public async Task<UserDto> GetUserById(Guid id)
        //{
        //    var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == id.ToString());

        //    var userDto = _mapper.Map<User, UserDto>(user);

        //    if (userDto != null)
        //        userDto.Role = await GetUserRole(userDto.Email);

        //    return await Task.FromResult(userDto);
        //}

        public async Task<ResultDto<bool>> EnviarEmailRecuperarSenha(string email)
        {
            var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return ResultDto<bool>.Validation("Email não cadastrado no sistema!");
            else if (user != null && user.IsGoogle)
                return ResultDto<bool>.Validation("Email já cadastrado via google!");
            else if (user != null && user.IsFacebook)
                return ResultDto<bool>.Validation("Email já cadastrado via facebook!");            

            var token = await GerarToken(user);
            _emailService.Send(user.Email, "Alteração de senha", EmailTemplate.RecuperarSenha(_configuration, user, token));
            return ResultDto<bool>.Success(true);
        }

        public async Task<ResultDto<bool>> RecuperarSenha(RecuperarSenhaDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            var base64EncodedBytes = System.Convert.FromBase64String(dto.Token);
            dto.Token = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NovaSenha);

            if (!result.Succeeded)
                return ResultDto<bool>.Validation("Erro ao alterar senha!");

            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        private async Task<UserDto> GetUserByEmail(string email)
        {            
            var user = await _userManager?.Users?.FirstOrDefaultAsync(c => c.Email == email);

            var userDto = _mapper.Map<User, UserDto>(user);

            if (userDto != null)
                userDto.Role = await GetUserRole(email);

            return await Task.FromResult(userDto);           
        }             

        private async Task<string> GerarToken(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(token);
            return await Task.FromResult(Convert.ToBase64String(plainTextBytes));
        }

        private async Task AddUserRole(UserDto userDto)
        {
            var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            await _userManager.AddToRoleAsync(user, userDto.Role);
        }

        private async Task<string> GetUserRole(string email)
        {
            var user = await _userManager.Users?.FirstOrDefaultAsync(c => c.Email == email);
            var roles = await _userManager.GetRolesAsync(user);
            return await Task.FromResult(roles.FirstOrDefault());            
        }

        private async Task<IList<Claim>> GetUserClaims(string email)
        {
            var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.UserName == email);
            var claims = await _userManager.GetClaimsAsync(user);
            return await Task.FromResult(claims);
        }

        //private async Task RemoveAllUserRoles(string id)
        //{
        //    var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == id);
        //    var roles = await _userManager.GetRolesAsync(user);
        //    await _userManager.RemoveFromRolesAsync(user, roles);
        //}

        //private async Task RemoveAndAddUserRoles(UserDto userDto)
        //{
        //    await RemoveAllUserRoles(userDto.Id);
        //    await AddUserRole(userDto);
        //}

        //public async Task Delete(Guid id)
        //{
        //    var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == id.ToString());

        //    if (user != null)
        //        await _userManager.DeleteAsync(user);
        //}         

        //public async Task<UserDto> UpdateLoggedUserData(UserDto userDto)
        //{
        //    var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == userDto.Id);

        //    user.Nome = userDto.Nome;
        //    user.Email = userDto.Email;

        //    await _userManager.UpdateAsync(user);

        //    return await Task.FromResult(userDto);
        //}

        //public async Task<UserDto> ObterDadosUsuarioLogado(Guid id)
        //{
        //    var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == id.ToString());
        //    return await Task.FromResult(_mapper.Map<User, UserDto>(user));
        //}

        //public async Task<bool> AtualizarSenha(UserDto userDto)
        //{
        //    var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == userDto.Id);

        //    var result = await _userManager.ChangePasswordAsync(user, userDto.Senha, userDto.NovaSenha);

        //    return await Task.FromResult(result.Succeeded);
        //}

        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
