using Diplom.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace Diplom.Services.Implementations
{
    public class EmailService:IEmailService
    {

        public EmailService() { }



        public async Task<bool> SendEmailAsync(string email, string subject, string toMessage)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Diplom sender","roman-maslov423@yandex.ru"));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;

                message.Body = new TextPart("plain")
                {
                    Text = toMessage
                };

                using (var client  = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.yandex.ru", 465, true);
                    await client.AuthenticateAsync("roman-maslov423@yandex.ru", @"ubhbyixqtlibddwk");
                    await client.SendAsync(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
