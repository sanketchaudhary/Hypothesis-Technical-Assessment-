using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechnicalAssessment.Business.EmailProvider;
using TechnicalAssessment.Business.EmailProvider.Impl;
using TechnicalAssessment.Models.EmailProvider;

namespace TechnicalAssignment.Controllers
{
    public class EmailController : ApiController
    {
        #region Properties
        IEmailManager _emailManager;
        #endregion

        #region Constructors
        public EmailController() : this(new EmailManager())
        { }

        public EmailController(IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }
        #endregion

        /// <summary>
        /// Web method to send email
        /// </summary>
        /// <param name="requestModel">Request model holding user details</param>
        /// <returns>Http response message</returns>
        [HttpPost]
        public HttpResponseMessage SendEmail(EmailRequestModel requestModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(requestModel?.Email))
                {
                    // Call manager method to send email
                    var emailSent = _emailManager.SendEmail(requestModel);

                    // Create Web Response
                    return Request.CreateResponse<bool>(HttpStatusCode.OK, emailSent);
                }
                else
                {
                    // If request model is null, create Error Web response
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Input model is null");
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Exception occured while sending email: " + ex);
            }
        }
    }
}
