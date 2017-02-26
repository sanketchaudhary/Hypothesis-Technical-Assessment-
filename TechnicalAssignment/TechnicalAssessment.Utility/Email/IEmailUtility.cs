using RestSharp;
using TechnicalAssessment.Models.EmailProvider;

namespace TechnicalAssessment.Utility.Email
{
    public interface IEmailUtility
    {
        RestResponse SendEmailWithMailgun(EmailRequestModel emailDetails);

        /// <summary>
        /// Method to send email using SendGrid service
        /// </summary>
        /// <param name="emailDetails">Email details</param>
        /// <returns></returns>
        void SendEmailWithSendGrid(EmailRequestModel emailDetails);
    }
}
