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
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IEmailService _emailService;               

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(SignInManager<User> signInManager,
                                   UserManager<User> userManager,
                                   SigningConfigurations signingConfigurations, 
                                   TokenConfigurations tokenConfigurations,
                                   IEmailService emailService,
                                   IConfiguration configuration,
                                   IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _emailService = emailService;
            _configuration = configuration;
            _mapper = mapper;
        }
       
        public async Task<ResultDto<AuthenticatedDto>> Login(AuthDto authDto)
        {
            var success = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Senha, false, false);            

            if (!success.Succeeded)                           
                return ResultDto<AuthenticatedDto>.Validation("Login ou senha inválidos!"); 
           
            var applicationUser = await GetUserByEmail(authDto.Email);

            if(!applicationUser.Estabelecimentos.Any(x => x.EstabelecimentoId == authDto.EstabelecimentoId))
                return ResultDto<AuthenticatedDto>.Validation("Usuário não vinculado ao estabalecimento!");                       

            var userDto = new UserDto
            {
                Id = applicationUser.Id,
                Nome = applicationUser.Nome,
                Email = applicationUser.Email,
                Role = applicationUser.Role,
                Claims = applicationUser.Claims,
                EstabelecimentoId = authDto.EstabelecimentoId,
                EstabelecimentoNomeUrl = applicationUser.Estabelecimentos.FirstOrDefault(x => x.EstabelecimentoId == authDto.EstabelecimentoId).Url,
                EstabelecimentoNome = applicationUser.Estabelecimentos.FirstOrDefault(x => x.EstabelecimentoId == authDto.EstabelecimentoId).Nome
            };

            return ResultDto<AuthenticatedDto>.Success(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));
        }

        public async Task<ResultDto<int>> GetTotalCadastrado(int? estabelecimentoId, string usuarioId)
        {
            var totalCadastrado = await _userManager.Users
                   .Include(x => x.UserEstabelecimentos)
                   .CountAsync(x => x.UserEstabelecimentos.Any(x => !estabelecimentoId.HasValue || x.EstabelecimentoId == estabelecimentoId) &&
                   (string.IsNullOrEmpty(usuarioId) || x.Id == usuarioId));

            return await Task.FromResult(ResultDto<int>.Success(totalCadastrado));
        }

        public async Task<ResultDto<AuthenticatedDto>> LoginAdmin(AuthDto authDto)
        {
            var success = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Senha, false, false);

            if (!success.Succeeded)
                return ResultDto<AuthenticatedDto>.Validation("Login ou senha inválidos!");

            var applicationUser = await GetUserByEmail(authDto.Email);

            if (applicationUser.Role != "Administrador")
                return ResultDto<AuthenticatedDto>.Validation("Perfil não é Administrador!");

            var userDto = new UserDto
            {
                Id = applicationUser.Id,
                Nome = applicationUser.Nome,
                Email = applicationUser.Email,
                Role = applicationUser.Role,
                Claims = applicationUser.Claims
            };

            return ResultDto<AuthenticatedDto>.Success(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));
        }
        public async Task<ResultDto<AuthenticatedDto>> LoginSocial(AuthDto authDto)
        {
            var success = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Senha, false, false);
            UserDto userDto;

            if (!success.Succeeded)
            {
                var result = await SalvarCliente(new UserDto()
                {
                    Email = authDto.Email,
                    Senha = authDto.Senha,
                    Nome = authDto.Nome,
                    EstabelecimentoId = authDto.EstabelecimentoId,
                    IsFacebook = authDto.IsFacebook,
                    IsGoogle = authDto.IsGoogle
                });

                if (result.StatusCode != 200)
                    return await Task.FromResult(ResultDto<AuthenticatedDto>.Validation(result.Errors));
                
                userDto = result.Data;
            }
            else
            {
                var applicationUser = await GetUserByEmail(authDto.Email);

                if (!applicationUser.Estabelecimentos.Any(x => x.EstabelecimentoId == authDto.EstabelecimentoId))
                {
                    var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == applicationUser.Id);
                    var userEstabelecimento = new UserEstabelecimento() { EstabelecimentoId = authDto.EstabelecimentoId };
                    user.AdicionarEstabelecimento(userEstabelecimento); 
                    await _userManager.UpdateAsync(user);

                    applicationUser = await GetUserByEmail(authDto.Email);
                    userDto = new UserDto
                    {
                        Id = applicationUser.Id,
                        Nome = applicationUser.Nome,
                        Email = applicationUser.Email,
                        Role = applicationUser.Role,
                        Claims = applicationUser.Claims,
                        EstabelecimentoId = authDto.EstabelecimentoId,
                        EstabelecimentoNomeUrl = applicationUser.Estabelecimentos.FirstOrDefault(x => x.EstabelecimentoId == authDto.EstabelecimentoId).Url,
                        EstabelecimentoNome = applicationUser.Estabelecimentos.FirstOrDefault(x => x.EstabelecimentoId == authDto.EstabelecimentoId).Nome
                    };

                    _emailService.Send(userDto.Email, "Confirmação de cadastro", userDto.EstabelecimentoNome, EmailTemplate.ConfirmacaoCadastro(_configuration, userDto));
                }  
                else
                {
                    userDto = new UserDto
                    {
                        Id = applicationUser.Id,
                        Nome = applicationUser.Nome,
                        Email = applicationUser.Email,
                        Role = applicationUser.Role,
                        Claims = applicationUser.Claims,
                        EstabelecimentoId = authDto.EstabelecimentoId,
                        EstabelecimentoNomeUrl = applicationUser.Estabelecimentos.FirstOrDefault(x => x.EstabelecimentoId == authDto.EstabelecimentoId).Url,
                        EstabelecimentoNome = applicationUser.Estabelecimentos.FirstOrDefault(x => x.EstabelecimentoId == authDto.EstabelecimentoId).Nome
                    };
                }              
            }

            return ResultDto<AuthenticatedDto>.Success(TokenWrite.WriteToken(userDto, _tokenConfigurations, _signingConfigurations));
        }
        public async Task<ResultDto<IEnumerable<UserDto>>> ObterTodosClientes(int estabelecimentoId)
        {            
            var clientesPorEstabelecimento = await _userManager.Users
                    .Include(x => x.UserEstabelecimentos)                    
                    .Where(x => x.UserEstabelecimentos.Any(x => x.EstabelecimentoId == estabelecimentoId))
                    .ToListAsync();           

            if (!clientesPorEstabelecimento.Any())
                return ResultDto<IEnumerable<UserDto>>.Validation("Usuário não encontrado na base de dados!");

            var userDto = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(clientesPorEstabelecimento);
            return await Task.FromResult(ResultDto<IEnumerable<UserDto>>.Success(userDto));
        }

        public async Task<ResultDto<IEnumerable<UserDto>>> ObterTodos(int? estabelecimentoId)
        {
            var clientesPorEstabelecimento = await _userManager.Users
                    .Include(x => x.UserEstabelecimentos)
                        .ThenInclude(x => x.Estabelecimento)                   
                    .Where(x => !estabelecimentoId.HasValue || x.UserEstabelecimentos.Any(x => x.EstabelecimentoId == estabelecimentoId))
                    .ToListAsync();          

            if (!clientesPorEstabelecimento.Any())
                return ResultDto<IEnumerable<UserDto>>.Validation("Usuário não encontrado na base de dados!");

            var usersDto = new List<UserDto>();

            foreach (var usuario in clientesPorEstabelecimento)
            {
                var userDto = _mapper.Map<User, UserDto>(usuario);                
                var roles = await _userManager.GetRolesAsync(usuario);
                var claims = await _userManager.GetClaimsAsync(usuario);
                   
                userDto.Role = roles.FirstOrDefault();
                userDto.Claims = claims;

                usersDto.Add(userDto);
            }
           
            return await Task.FromResult(ResultDto<IEnumerable<UserDto>>.Success(usersDto));
        }

        public async Task<ResultDto<UserDto>> SalvarUsuario(UserDto userDto)
        {
            var userDtoValidate = new UserDtoValidate(userDto);
            if (!userDtoValidate.Validate())
                return ResultDto<UserDto>.Validation(userDtoValidate.Mensagens);

            var usuarioJaCadastrado = await _userManager.Users.FirstOrDefaultAsync(c => c.Email == userDto.Email);

            if (usuarioJaCadastrado != null && usuarioJaCadastrado.IsGoogle)
                return ResultDto<UserDto>.Validation("Email já cadastrado via google!");
            else if (usuarioJaCadastrado != null && usuarioJaCadastrado.IsFacebook)
                return ResultDto<UserDto>.Validation("Email já cadastrado via facebook!");
            else if (usuarioJaCadastrado != null)
                return ResultDto<UserDto>.Validation("Email já cadastrado!");
                        
            var user = new User(userDto);
            if (userDto.Role != "Administrador")
            {
                var userEstabelecimentos = new List<UserEstabelecimento> { new UserEstabelecimento() { EstabelecimentoId = userDto.EstabelecimentoId } };
                user.VincularEstabelecimento(userEstabelecimentos);
            }

            var result = await _userManager.CreateAsync(user, userDto.Senha);
            userDto.Id = user.Id;

            if (result.Succeeded && !string.IsNullOrWhiteSpace(userDto.Role))
            {
                var userDB = await _userManager.Users
                    .Include(x => x.UserEstabelecimentos)
                    .ThenInclude(x => x.Estabelecimento)
                    .FirstOrDefaultAsync(u => u.Id == userDto.Id);
                var estabelecimento = userDB.UserEstabelecimentos.FirstOrDefault().Estabelecimento;

                userDto.EstabelecimentoNomeUrl = estabelecimento.Url;
                userDto.EstabelecimentoNome = estabelecimento.Nome;

                await _userManager.AddToRoleAsync(userDB, userDto.Role);
                _emailService.Send(userDto.Email, "Confirmação de cadastro", userDto.EstabelecimentoNome, EmailTemplate.ConfirmacaoCadastro(_configuration, userDto));
            }

            return await Task.FromResult(ResultDto<UserDto>.Success(userDto));
        }

        public async Task<ResultDto<UserDto>> SalvarCliente(UserDto userDto)
        {
            userDto.Role = "Cliente";
            var result = await Salvar(userDto);

            if(result.StatusCode == 200)
                _emailService.Send(userDto.Email, "Confirmação de cadastro", result.Data.EstabelecimentoNome, EmailTemplate.ConfirmacaoCadastro(_configuration, result.Data));
            
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
            var userEstabelecimentos = new List<UserEstabelecimento> { new UserEstabelecimento() { EstabelecimentoId = userDto.EstabelecimentoId }};
            var user = new User(userDto);
            user.VincularEstabelecimento(userEstabelecimentos);

            var result = await _userManager.CreateAsync(user, userDto.Senha);
            userDto.Id = user.Id;

            if (result.Succeeded && !string.IsNullOrWhiteSpace(userDto.Role))
            {
                var userDB = await _userManager.Users
                    .Include(x => x.UserEstabelecimentos)
                    .ThenInclude(x => x.Estabelecimento)
                    .FirstOrDefaultAsync(u => u.Id == userDto.Id);

                var estabelecimento = userDB.UserEstabelecimentos.FirstOrDefault().Estabelecimento;
                userDto.EstabelecimentoNomeUrl = estabelecimento.Url;
                userDto.EstabelecimentoNome = estabelecimento.Nome;

                await _userManager.AddToRoleAsync(userDB, userDto.Role);
            }   
            else if(result.Errors.Any(x => x.Code == "PasswordTooShort"))
                return ResultDto<UserDto>.Validation("senha deve ter no minimo 6 caracteres!");
            else
                return ResultDto<UserDto>.Validation("Cadastro inválido!");

            return await Task.FromResult(ResultDto<UserDto>.Success(userDto));
        }

        public async Task<ResultDto<UserDto>> Update(UserDto userDto)
        {
            var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == userDto.Id);

            if (user == null)
                return ResultDto<UserDto>.Validation("Usuário não encontrado na base de dados!");

            user.AtualizarUsuario(userDto);

            await _userManager.UpdateAsync(user);

            return await Task.FromResult(ResultDto<UserDto>.Success(userDto));
        }

        public async Task<ResultDto<UserDto>> GetUserById(string id)
        {
            var user = await _userManager.Users?
                .Include(x => x.Endereco)
                    .ThenInclude(x => x.Municipio)
                        .ThenInclude(x => x.Estado)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return ResultDto<UserDto>.Validation("Usuário não encontrado na base de dados!");

            var userDto = _mapper.Map<User, UserDto>(user);

            if (userDto != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Role = roles.FirstOrDefault();
            }                

            return await Task.FromResult(ResultDto<UserDto>.Success(userDto));
        }

        public async Task<ResultDto<bool>> EnviarEmailRecuperarSenha(string email, int estabelecimentoId)
        {
            var user = await _userManager.Users?
                .Include(x => x.UserEstabelecimentos)
                    .ThenInclude(x => x.Estabelecimento)
                .FirstOrDefaultAsync(u => u.Email == email && u.UserEstabelecimentos.Any(x => x.EstabelecimentoId == estabelecimentoId));
            
            if (user == null)
                return ResultDto<bool>.Validation("Usuário não cadastrado no sistema!");
            else if (user != null && user.IsGoogle)
                return ResultDto<bool>.Validation("Usuário já cadastrado via google!");
            else if (user != null && user.IsFacebook)
                return ResultDto<bool>.Validation("Usuário já cadastrado via facebook!");

            var userEstabelecimento = user.UserEstabelecimentos.FirstOrDefault(x => x.EstabelecimentoId == estabelecimentoId);
            var token = await GerarToken(user);
            _emailService.Send(user.Email, "Alteração de senha", userEstabelecimento?.Estabelecimento?.Nome, EmailTemplate.RecuperarSenha(_configuration, user, token, userEstabelecimento?.Estabelecimento?.Url));
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
            else if (result.Errors.Any(x => x.Code == "InvalidToken"))
                return ResultDto<bool>.Validation("Token Invalido!");

            return await Task.FromResult(ResultDto<bool>.Success(true));
        }

        public async Task<ResultDto<bool>> AtualizarSenha(UserDto userDto)
        {
            var user = await _userManager.Users?.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (user == null)
                return ResultDto<bool>.Validation("Usuário não encontrado na base de dados!");

            var result = await _userManager.ChangePasswordAsync(user, userDto.Senha, userDto.NovaSenha);

            return await Task.FromResult(ResultDto<bool>.Success(result.Succeeded));
        }

        public async Task<ResultDto<bool>> Delete(string usuarioId)
        {
            var user = await _userManager.Users
                 .Include(x => x.Agendamentos)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (user == null)
                return ResultDto<bool>.Validation("Usuário não encontrado na base de dados!");

            if (user.Agendamentos.Any())
                return ResultDto<bool>.Validation("Usuário possui agendamentos vinculados!");

            await _userManager.DeleteAsync(user);

            return await Task.FromResult(ResultDto<bool>.Success(true));
        }        

        private async Task<UserDto> GetUserByEmail(string email)
        {            
            var user = await _userManager?.Users?
                .Include(x => x.UserEstabelecimentos)
                    .ThenInclude(x => x.Estabelecimento)
                .FirstOrDefaultAsync(c => c.Email == email);

            var userDto = _mapper.Map<User, UserDto>(user);

            if (userDto != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);
                var estabelecimentos = user.UserEstabelecimentos
                    .Select(x => new EstabelecimentoDto()
                    {
                        EstabelecimentoId = x.EstabelecimentoId,
                        Nome = x.Estabelecimento.Nome,
                        Url = x.Estabelecimento.Url
                    });

                userDto.Role = roles.FirstOrDefault();
                userDto.Claims = claims;
                userDto.Estabelecimentos = estabelecimentos;
               
            }                

            return await Task.FromResult(userDto);           
        }             

        private async Task<string> GerarToken(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(token);
            return await Task.FromResult(Convert.ToBase64String(plainTextBytes));
        }                

        public async Task LogOff()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
