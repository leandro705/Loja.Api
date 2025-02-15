﻿using Loja.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Loja.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _sendApiKey;

        public EmailService(IConfiguration configuration)
        {
            _sendApiKey = configuration["SendGrid:SmtpApiKey"];
        }

        public async void Send(string toAddress, string subject, string nomeLoja, string body, bool sendAsync = true)
        {            
            var client = new SendGridClient(_sendApiKey);
            var from = new EmailAddress("leandro_705@hotmail.com", "Contato - " + nomeLoja);
            var to = new EmailAddress(toAddress, toAddress);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", body);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
