using System;
using RestSharp;
using TechnicalAssessment.Models.EmailProvider;
using System.Configuration;
using RestSharp.Authenticators;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TechnicalAssessment.Utility.Email.Impl
{
    public class EmailUtility : IEmailUtility
    {
        public RestResponse SendEmailWithMailgun(EmailRequestModel emailDetails)
        {
            try
            {
                // Rest client 
                var client = new RestClient(ConfigurationManager.AppSettings["MailgunUrl"].ToString())
                {
                    Authenticator = new HttpBasicAuthenticator("api", ConfigurationManager.AppSettings["MailgunAuthenticator"].ToString())
                };

                // Request object for Rest call
                var request = new RestRequest()
                {
                    Resource = "{domain}/messages",
                    Method = Method.POST
                };
                request.AddParameter("domain", ConfigurationManager.AppSettings["MailgunDomain"].ToString(), ParameterType.UrlSegment);
                request.AddParameter("from", "Hypothesis Technical Assessment <" + ConfigurationManager.AppSettings["MailgunFrom"].ToString() + ">");
                request.AddParameter("to", emailDetails.Email);
                request.AddParameter("cc", ConfigurationManager.AppSettings["CCEmail"].ToString());
                request.AddParameter("subject", ConfigurationManager.AppSettings["MailSubject"].ToString());
                request.AddParameter("text", string.Format(ConfigurationManager.AppSettings["EmailText"].ToString(), emailDetails.Name));
                return (RestResponse)client.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to send email using SendGrid service
        /// </summary>
        /// <param name="emailDetails">Email details</param>
        /// <returns></returns>
        public void SendEmailWithSendGrid(EmailRequestModel emailDetails)
        {
            try
            {
                // Create Sendgrid client object
                var client = new SendGridClient(ConfigurationManager.AppSettings["SendGridApiKey"].ToString());

                // Call method from mailhelper class, set recepient details and send email
                var msg = MailHelper.CreateSingleEmail(
                    new EmailAddress(ConfigurationManager.AppSettings["SendGridFrom"].ToString(), "Hypothesis Technical Assessment"),
                    new EmailAddress(emailDetails.Email, "Example User"),
                    ConfigurationManager.AppSettings["MailSubject"].ToString(),
                    string.Empty,
                    string.Format(ConfigurationManager.AppSettings["EmailText"].ToString(), emailDetails.Name));

                // Call SendEmailAsync() method and send email
                client.SendEmailAsync(msg).Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
