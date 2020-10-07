using Loja.CrossCutting.Dto;
using Loja.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Loja.Application.Templates
{
    public static class EmailTemplate
    {
        public static string ConfirmacaoCadastro(IConfiguration configuration, UserDto userDto)
        {
            var corpoEmail = $"<p>Olá {userDto.Nome}, <br> <br> Você foi cadastrado na nossa plataforma e agora pode acompanhar todos os " +
                    $"dados de agendamentos realizados! " +
                    $"<br> " +
                    $"<label style='font-weight: bold;'>Acesse através do link: <a href='{configuration["Urls:Site"]}/Login/Index/{userDto.EstabelecimentoNomeUrl}'>Entrar</a> </label>" +
                    $"<br> <br>";
            return corpoEmail;
        }

        public static string RecuperarSenha(IConfiguration configuration, User user, string token, string nomeUrl)
        {
            var corpoEmail = $"<p>Olá {user.Nome}, <br/> <br/> Você solicitou a recuperação de senha." +
                             $"<br/> </br>" +
                             $"<label style='font-weight: bold;'>Altere sua senha através do link: <a href='{configuration["Urls:Site"]}/recuperar-senha/atualizar-senha?token={token}&id={user.Id}&nomeUrl={nomeUrl}'>Cadastrar Nova Senha</a> </label>" +
                             $"<br/> </br></br>";
                             
            return corpoEmail;
        }
    }
}
