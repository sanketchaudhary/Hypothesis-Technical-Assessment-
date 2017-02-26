using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using TechnicalAssessment.Business.EmailProvider;
using TechnicalAssessment.Models.EmailProvider;
using TechnicalAssignment.Controllers;

namespace TechnicalAssessment.UnitTest.Controller
{
    [TestFixture]
    public class EmailControllerTest
    {
        /// <summary>
        /// Test case to check positive scenario
        /// </summary>
        [Test]
        public void SendEmailTest()
        {
            // Mock Manager layer and  stub the business method
            var mockManager = new Mock<IEmailManager>();
            mockManager.Setup(x => x.SendEmail(It.IsAny<EmailRequestModel>())).Returns((true));

            // Arrange
            var requestModel = new EmailRequestModel()
            {
                Name = "Sanket Chaudhary",
                Email = "sanket.chaudhary@gmail.com"
            };

            // Create controller instance
            var api = new EmailController(mockManager.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            // Act
            var responseObj = api.SendEmail(requestModel);
            var responseModel = responseObj?.Content?.ReadAsAsync<bool>()?.Result;

            // Assert
            Assert.AreEqual(responseModel, true);
        }

        /// <summary>
        /// Test case to check null email object scenario
        /// </summary>
        [Test]
        public void SendEmailTestForNullRequestModel()
        {
            // Mock Manager layer and  stub the business method
            var mockManager = new Mock<IEmailManager>();

            // Arrange
            EmailRequestModel requestModel = null;

            // Create controller instance
            var api = new EmailController(mockManager.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            // Act
            var responseObj = api.SendEmail(requestModel);
            var statusCode = responseObj?.StatusCode;

            // Assert
            Assert.AreEqual(statusCode, HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Test case to check empty emailId scenario
        /// </summary>
        [Test]
        public void SendEmailTestForEmptyEmailId()
        {
            // Mock Manager layer and  stub the business method
            var mockManager = new Mock<IEmailManager>();

            // Arrange
            var requestModel = new EmailRequestModel()
            {
                Name = "Sanket",
                Email = ""
            };

            // Create controller instance
            var api = new EmailController(mockManager.Object)
            {
                Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                }
            };

            // Act
            var responseObj = api.SendEmail(requestModel);
            var statusCode = responseObj?.StatusCode;

            // Assert
            Assert.AreEqual(statusCode, HttpStatusCode.BadRequest);
        }
    }
}
