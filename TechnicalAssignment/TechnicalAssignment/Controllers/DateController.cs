using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechnicalAssessment.Business.Date;
using TechnicalAssessment.Business.Date.Impl;
using TechnicalAssessment.Models.Date;

namespace TechnicalAssignment.Controllers
{
    public class DateController : ApiController
    {
        #region Properties
        IDateManager _dateManager;
        #endregion

        #region Constructors
        public DateController() : this(new DateManager())
        { }

        public DateController(IDateManager dateManager)
        {
            _dateManager = dateManager;
        }
        #endregion

        /// <summary>
        /// Web method to calculate difference in dates
        /// </summary>
        /// <param name="requestModel">Input request model having From and To date</param>
        /// <returns>Http WebResponse having date difference</returns>
        [HttpPost]
        public HttpResponseMessage GetDateDifference(DateRequestModel requestModel)
        {
            try
            {
                if (requestModel != null)
                {
                    // Call manager method to calculate date difference in days
                    var days = _dateManager.CalculateDateDifference(requestModel);

                    // Create Web Response
                    return Request.CreateResponse<int>(HttpStatusCode.OK, days);
                }
                else
                {
                    // If request model is null, create Error Web response
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Input model is null");
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Exception occured while retrieving date difference: " + ex);
            }
        }
    }
}
