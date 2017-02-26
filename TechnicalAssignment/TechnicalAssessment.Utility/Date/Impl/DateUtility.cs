using model = TechnicalAssessment.Models.Date;

namespace TechnicalAssessment.Utility.Date.Impl
{
    public class DateUtility : IDateUtility
    {
        // Array holding the days in each month
        int[] daysInMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// Method to get days counts for given date
        /// </summary>
        /// <param name="date">Date object</param>
        /// <returns>Days count from given date</returns>
        public int GetDaysCountTillGivenDate(model.Date date)
        {
            // Multiply year value with 365 and add days 
            int days = date.Year * 365 + date.Day;

            // Calculate days till given month and add then in total days count
            for (int i = 0; i < date.Month - 1; i++)
                days += daysInMonth[i];

            // Calculate leap year count and add a day for each year in days count
            days += GetLeapYearCount(date);

            // Return day count
            return days;
        }

        /// <summary>
        /// Method to calculate leap year count till given date
        /// </summary>
        /// <param name="date">Date object</param>
        /// <returns>Leap year count</returns>
        private int GetLeapYearCount(model.Date date)
        {
            // Get  year component from date
            int totalYears = date.Year;

            // If selected month is Jan or Feb, do not consider selected year for calculating leap years
            if (date.Month <= 2)
            {
                totalYears--;
            }

            // Get total number of leap years till given year
            return totalYears / 4 - totalYears / 100 + totalYears / 400;
        }
    }
}
