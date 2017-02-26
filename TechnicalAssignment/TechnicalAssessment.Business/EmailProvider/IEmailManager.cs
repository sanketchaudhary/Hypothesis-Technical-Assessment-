using TechnicalAssessment.Models.EmailProvider;

namespace TechnicalAssessment.Business.EmailProvider
{
    public interface IEmailManager
    {
        /// <summary>
        /// Method to send Emails using thrird party email services
        /// </summary>
        /// <param name="emailDetails">Email details</param>
        /// <returns></returns>
        bool SendEmail(EmailRequestModel emailDetails);
    }
}
