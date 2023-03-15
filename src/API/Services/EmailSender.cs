using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MimeKit.Text;


namespace API.Services;

//https://ethereal.email/create

public static class EmailSender 
{
    public static async Task SendVerificationEmail(string email, string subject, string htmlMessage)
    {
        var emailToSend = new MimeMessage() ;

        emailToSend.From.Add(MailboxAddress.Parse("rowena68@ethereal.email"));
        emailToSend.To.Add(MailboxAddress.Parse("rowena68@ethereal.email"));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(TextFormat.Html){Text = htmlMessage};

        using var emailClient = new SmtpClient();
        //Default smtp for google is 587 should create static class for all smtp
        //emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        emailClient.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
        emailClient.Authenticate("rowena68@ethereal.email", "2AeZ1aUeUU8SRWwSzr");
        await emailClient.SendAsync(emailToSend);
        emailClient.Disconnect(true);
        
        return;
    }
}