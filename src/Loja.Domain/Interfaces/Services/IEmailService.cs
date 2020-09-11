namespace Loja.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        void Send(string toAddress, string subject, string body, bool sendAsync = true);
    }
}
