using Microsoft.Extensions.Logging;

namespace DutchTreat.Services
{
    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> _logger;
        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }

        public void SendEMail(string to, string subject, string from, string body){
            // log the message
            _logger.LogInformation($"To: {to} Subject {subject} Body{body}");
        }
    }

}