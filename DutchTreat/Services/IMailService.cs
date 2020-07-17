namespace DutchTreat.Services
{
    public interface IMailService
    {
        void SendEMail(string to, string subject, string from, string body);
    }

}