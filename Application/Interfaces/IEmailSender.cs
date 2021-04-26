using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailSender
    {
        #region BaseClass

        Task SendEmailAsync(string email, string subject, string message);

        #endregion

        #region CustomMethods

        Task SendEmailAsync<T>(string email, string subject, T model);

        #endregion
    }
}
