namespace MailService.MailSetting;

public class SocialEmailModel
{
    public string? From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string? Host { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}