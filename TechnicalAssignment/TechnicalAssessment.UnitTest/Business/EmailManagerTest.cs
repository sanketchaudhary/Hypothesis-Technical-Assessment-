using Moq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechnicalAssessment.Business.EmailProvider.Impl;
using TechnicalAssessment.Models.EmailProvider;
using TechnicalAssessment.Utility.Email;

namespace TechnicalAssessment.UnitTest.Business
{
    [TestFixture]
    public class EmailManagerTest
    {
        /// <summary>
        /// Test to calculate send email postive scenario with Mailgun
        /// </summary>
        [Test]
        public void SendEmailMailgunTest()
        {
            // Mock Utility layer and  stub the business method
            var mockUtility = new Mock<IEmailUtility>();
            mockUtility.Setup(x => x.SendEmailWithMailgun(It.IsAny<EmailRequestModel>())).Returns((new RestResponse() { StatusCode = HttpStatusCode.OK }));
            mockUtility.Setup(x => x.SendEmailWithSendGrid(It.IsAny<EmailRequestModel>()));

            // Arrange
            var emailModel = new EmailRequestModel()
            {
                Name = "Sanket Chaudhary",
                Email = "sanket.chaudhary@gmail.com"
            };

            // Create business class instance
            var manager = new EmailManager(mockUtility.Object);

            // Act
            var responseObj = manager.SendEmail(emailModel);

            // Assert
            Assert.AreEqual(responseObj, true);
        }

        /// <summary>
        /// Test to calculate send email postive scenario with SendGrid
        /// </summary>
        [Test]
        public void SendEmailSendGridTest()
        {
            // Mock Utility layer and  stub the business method
            var mockUtility = new Mock<IEmailUtility>();
            mockUtility.Setup(x => x.SendEmailWithMailgun(It.IsAny<EmailRequestModel>())).Returns((new RestResponse() { StatusCode = HttpStatusCode.BadRequest }));
            mockUtility.Setup(x => x.SendEmailWithSendGrid(It.IsAny<EmailRequestModel>()));

            // Arrange
            var emailModel = new EmailRequestModel()
            {
                Name = "Sanket Chaudhary",
                Email = "sanket.chaudhary@gmail.com"
            };

            // Create business class instance
            var manager = new EmailManager(mockUtility.Object);

            // Act
            var responseObj = manager.SendEmail(emailModel);

            // Assert
            Assert.AreEqual(responseObj, true);
        }
    }
}
