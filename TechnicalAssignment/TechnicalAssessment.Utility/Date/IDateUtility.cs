using model = TechnicalAssessment.Models.Date;

namespace TechnicalAssessment.Utility.Date
{
    public interface IDateUtility
    {
        /// <summary>
        /// Method to get days counts for given date
        /// </summary>
        /// <param name="date">Date object</param>
        /// <returns>Days count from given date</returns>
        int GetDaysCountTillGivenDate(model.Date date);
    }
}
