using TechnicalAssessment.Models.Date;

namespace TechnicalAssessment.Business.Date
{
    public interface IDateManager
    {
        /// <summary>
        /// Business method to calculate days difference between dates
        /// </summary>
        /// <param name="datesModel">Model holding From and To date</param>
        /// <returns>Days count</returns>
        int CalculateDateDifference(DateRequestModel datesModel);
    }
}
