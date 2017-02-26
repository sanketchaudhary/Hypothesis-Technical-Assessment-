using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using TechnicalAssessment.Business.Date;
using TechnicalAssessment.Models.Date;
using TechnicalAssignment.Controllers;

namespace TechnicalAssessment.UnitTest.Controller
{
    [TestFixture]
    public class DateControllerTest
    {
        /// <summary>
        /// Test case to check positive scenario
        /// </summary>
        [Test]
        public void GetDateDifferenceTest()
        {
            // Mock Manager layer and  stub the business method
            var mockManager = new Mock<IDateManager>();
            mockManager.Setup(x => x.CalculateDateDifference(It.IsAny<DateRequestModel>())).Returns((19));

            // Arrange
            var requestModel = new DateRequestModel() {
                FromDate = new Date() { Day = 2, Month = 6, Year = 1983 },
                ToDate = new Date() { Day = 22, Month = 6, Year = 1983 }
            };

            // Create controller instance
            var api = new DateController(mockManager.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            // Act
            var responseObj = api.GetDateDifference(requestModel);
            var responseModel = responseObj?.Content?.ReadAsAsync<int>()?.Result;

            // Assert
            Assert.AreEqual(responseModel, 19);
        }

        /// <summary>
        /// Test case to check null dates scenario
        /// </summary>
        [Test]
        public void GetDateDifferenceTestForNullRequestModel()
        {
            // Mock Manager layer and  stub the business method
            var mockManager = new Mock<IDateManager>();
            
            // Arrange
            DateRequestModel requestModel = null;

            // Create controller instance
            var api = new DateController(mockManager.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            // Act
            var responseObj = api.GetDateDifference(requestModel);
            var statusCode = responseObj?.StatusCode;

            // Assert
            Assert.AreEqual(statusCode, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Test case to check same dates scenario
        /// </summary>
        [Test]
        public void GetDateDifferenceTestForSameDate()
        {
            // Mock Manager layer and  stub the business method
            var mockManager = new Mock<IDateManager>();
            mockManager.Setup(x => x.CalculateDateDifference(It.IsAny<DateRequestModel>())).Returns((0));

            // Arrange
            var requestModel = new DateRequestModel()
            {
                FromDate = new Date() { Day = 2, Month = 6, Year = 1983 },
                ToDate = new Date() { Day = 2, Month = 6, Year = 1983 }
            };

            // Create controller instance
            var api = new DateController(mockManager.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            // Act
            var responseObj = api.GetDateDifference(requestModel);
            var responseModel = responseObj?.Content?.ReadAsAsync<int>()?.Result;

            // Assert
            Assert.AreEqual(responseModel, 0);
        }
    }
}
