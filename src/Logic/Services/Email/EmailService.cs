using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace Logic.Services.Email;

public class EmailService(IConfiguration configuration)
{
    private readonly string _server = configuration["Email:Server"]!;
    private readonly int _port = Convert.ToInt32(configuration["Email:Port"]!);
    private readonly string _username = configuration["Email:Username"]!;
    private readonly string _password = configuration["Email:Password"]!;

    public async Task SendEmail(SendEmailInput input, CancellationToken cancellationToken = default)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Web Cinema", "no-reply@cinema.com"));
        message.To.Add(new MailboxAddress(input.ToName, input.ToEmailAddress));
        message.Subject = input.Subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = input.Body
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_server, _port, SecureSocketOptions.StartTls, cancellationToken);
        await smtpClient.AuthenticateAsync(_username, _password, cancellationToken);
        _ = await smtpClient.SendAsync(message, cancellationToken);
        await smtpClient.DisconnectAsync(true, cancellationToken);
    }
}
