namespace MailService;

public interface INotificationService<in T> where T : class
{
    Task<bool> SendAsync(T model);
}