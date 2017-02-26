using Moq;
using NUnit.Framework;
using TechnicalAssessment.Business.Date.Impl;
using TechnicalAssessment.Models.Date;
using TechnicalAssessment.Utility.Date;

namespace TechnicalAssessment.UnitTest.Business
{
    [TestFixture]
    public class DateManagerTest
    {
        /// <summary>
        /// Test to calculate date difference for postive scenario
        /// </summary>
        [Test]
        public void CalculateDateDifferenceBusinessTest()
        {
            // Mock Utility layer and  stub the business method
            var mockUtility = new Mock<IDateUtility>();
            mockUtility.Setup(x => x.GetDaysCountTillGivenDate(It.IsAny<Date>())).Returns((1000));

            // Arrange
            var dateModel = new DateRequestModel()
            {
                FromDate = new Date() { Day = 2, Month = 6, Year = 1983 },
                ToDate = new Date() { Day = 22, Month = 6, Year = 1983 }
            };

            // Create business class instance
            var manager = new DateManager(mockUtility.Object);

            // Act
            var responseObj = manager.CalculateDateDifference(dateModel);

            // Assert
            Assert.AreEqual(responseObj, 0);
        }
    }
}
