using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace MailService.MailSetting;

public class SocialMailService : ISocialMailService
{
    public async Task<bool> SendAsync(SocialEmailModel model)
    {
        var success = false;
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(model.From));
            email.To.Add(MailboxAddress.Parse(model.To));
            email.Subject = model.Subject;
            email.Body = new TextPart(TextFormat.Html) {Text = model.Body};

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(model.Host,587,SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(model.UserName,model.Password);
            await smtp.SendAsync(email);
            // ReSharper disable once DisposeOnUsingVariable
            smtp.Dispose();
            success = true;
        }
        catch (Exception e)
        {
            success = false;
        }
        return success;
    }
}