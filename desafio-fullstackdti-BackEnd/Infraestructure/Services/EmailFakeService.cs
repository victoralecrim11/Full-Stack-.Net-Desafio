using System.IO;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class EmailFakeService : IEmailService
    {
        private readonly string _path;
        public EmailFakeService()
        {
            _path = "emails_fake.log";
        }

        public Task SendSaleNotificationAsync(string to, string subject, string body)
        {
            var linha = $"{System.DateTime.UtcNow:o} | To:{to} | Subject:{subject} | Body:{body}{System.Environment.NewLine}";
            File.AppendAllText(_path, linha);
            return Task.CompletedTask;
        }
    }
}
