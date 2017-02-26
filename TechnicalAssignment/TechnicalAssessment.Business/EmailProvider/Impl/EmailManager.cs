using RestSharp;
using System;
using System.Net;
using TechnicalAssessment.Models.EmailProvider;
using TechnicalAssessment.Utility.Email;
using TechnicalAssessment.Utility.Email.Impl;

namespace TechnicalAssessment.Business.EmailProvider.Impl
{
    public class EmailManager : IEmailManager
    {
        #region Properties
        IEmailUtility _emailUtility;
        #endregion

        #region Constructors
        public EmailManager() : this(new EmailUtility())
        { }

        public EmailManager(IEmailUtility emailUtility)
        {
            _emailUtility = emailUtility;
        }
        #endregion

        /// <summary>
        /// Method to send Emails using thrird party email services
        /// </summary>
        /// <param name="emailDetails">Email details</param>
        /// <returns></returns>
        public bool SendEmail(EmailRequestModel emailDetails)
        {
            RestResponse responseFromMailgun;
            bool responseReceivedFromMailgun = false;
            bool isSuccess = false;
            
            try
            {
                // Call SendEmailWithMailgun() method to send email
                responseFromMailgun = _emailUtility.SendEmailWithMailgun(emailDetails);

                // Set below flag to true if we recieve any response from Mailgun service
                responseReceivedFromMailgun = true;

                // If Mailgun service fails send email using SendGrid service
                if (responseFromMailgun.StatusCode != HttpStatusCode.OK)
                {
                    _emailUtility.SendEmailWithSendGrid(emailDetails);
                }

                isSuccess = true;
            }
            catch (Exception ex) {
                // If exception occurs while calling Mailgun service, send email using SendGrid service
                if (!responseReceivedFromMailgun)
                {
                    _emailUtility.SendEmailWithSendGrid(emailDetails);
                    isSuccess = true;
                }

                isSuccess = false;
            }

            return isSuccess;
        }
    }
}
