namespace Logic.Services.Email;

public record class SendEmailInput
{
    public required string ToName { get; init; }
    public required string ToEmailAddress { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
}
