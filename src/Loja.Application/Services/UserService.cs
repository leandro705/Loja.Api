using AutoMapper;
using Loja.Application.Templates;
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

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);           
            return usersDto;
        }

        public async Task<UserDto> Login(AuthDto authDto)
        {
            var success = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Password, false, false);

            if (!success.Succeeded)
                return null;

            var applicationUser = await GetUserByEmail(authDto.Email);
            var roles = await GetUserRoles(authDto.Email);
            var claims = await GetUserClaims(authDto.Email);

            return new UserDto
            {
                Id = applicationUser.Id,
                Nome = applicationUser.Nome,
                Email = applicationUser.Email,
                Roles = roles,
                Claims = claims
            };
        }

        public async Task<UserDto> LoginSocial(string email)
        {
            var applicationUser = await GetUserByEmail(email);

            if (applicationUser == null)
                return null;

            var roles = await GetUserRoles(email);
            var claims = await GetUserClaims(email);

            return new UserDto
            {
                Id = applicationUser.Id,
                Nome = applicationUser.Nome,
                Email = applicationUser.Email,
                Roles = roles,
                Claims = claims
            };
        }

        public async Task<UserDto> Create(UserDto userDto)
        {
            var user = new User
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                Nome = userDto.Nome
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            userDto.Id = user.Id;

            if (result.Succeeded && userDto.Roles != null && userDto.Roles.Any())
                await AddUserRoles(userDto);

            return userDto;
        }

        public async Task SendPasswordRecoveryEmail(string email)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Email == email);
            var token = await GenerateToken(user);
            _emailService.Send(user.Email, "Alteração de senha", EmailTemplate.RecoverPassword(_configuration, user, token));
        }

        private async Task<string> GenerateToken(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(token);
            return Convert.ToBase64String(plainTextBytes);
        }

        public async Task<UserDto> Update(UserDto userDto)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Id == userDto.Id);

            user.Nome = userDto.Nome;

            await _userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, userDto.Password);
            }

            await RemoveAndAddUserRoles(userDto);

            return userDto;
        }

        private async Task AddUserRoles(UserDto userDto)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Id == userDto.Id);

            await _userManager.AddToRolesAsync(user, userDto.Roles);
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Id == id.ToString());

            var userDto = _mapper.Map<User, UserDto>(user);

            if (userDto != null)
                userDto.Roles = await GetUserRoles(userDto.Email);

            return userDto;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userManager?.Users?.FirstOrDefaultAsync(c => c.Email == email);

                var userDto = _mapper.Map<User, UserDto>(user);

                if (userDto != null)
                    userDto.Roles = await GetUserRoles(email);

                return userDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> VerifyUserExistent(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == email);
        }

        public async Task<IList<string>> GetUserRoles(string email)
        {
            var user = await _userManager.Users?.FirstAsync(c => c.Email == email);

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IList<Claim>> GetUserClaims(string email)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.UserName == email);

            var claims = await _userManager.GetClaimsAsync(user);

            return claims;
        }

        public async Task RemoveAllUserRoles(string id)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Id == id);

            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, roles);
        }

        public async Task RemoveAndAddUserRoles(UserDto userDto)
        {
            await RemoveAllUserRoles(userDto.Id);

            await AddUserRoles(userDto);
        }

        public async Task Delete(Guid id)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Id == id.ToString());

            if (user != null)
                await _userManager.DeleteAsync(user);
        }

        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result.Succeeded;
        }

        public async Task<bool> ResetPassword(ResetPassowordDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            var base64EncodedBytes = System.Convert.FromBase64String(dto.Token);
            dto.Token = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            return result.Succeeded;
        }

        public async Task<UserDto> UpdateLoggedUserData(UserDto userDto)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Id == userDto.Id);

            user.Nome = userDto.Nome;
            user.Email = userDto.Email;

            await _userManager.UpdateAsync(user);

            return userDto;
        }

        public async Task<UserDto> ObterDadosUsuarioLogado(Guid id)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Id == id.ToString());

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<bool> AtualizarSenha(UserDto userDto)
        {
            var user = await _userManager.Users?.SingleAsync(u => u.Id == userDto.Id);

            var result = await _userManager.ChangePasswordAsync(user, userDto.Password, userDto.NewPassword);

            return result.Succeeded;
        }

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
