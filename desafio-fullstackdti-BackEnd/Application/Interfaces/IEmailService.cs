using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendSaleNotificationAsync(string to, string subject, string body);
    }
}
