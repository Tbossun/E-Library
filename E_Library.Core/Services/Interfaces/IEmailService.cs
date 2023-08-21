using E_Library.Data.Domains;

namespace E_Library.Core.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
