using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebbappLabb1EF.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Here can we  insert email logic/code
            return Task.CompletedTask;
        }
    }
}
