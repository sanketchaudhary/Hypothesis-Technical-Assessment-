using System;
using TechnicalAssessment.Utility.Date;
using TechnicalAssessment.Utility.Date.Impl;
using model = TechnicalAssessment.Models.Date;

namespace TechnicalAssessment.Business.Date.Impl
{
    public class DateManager : IDateManager
    {
        #region Properties
        IDateUtility _dateUtility;
        #endregion

        #region Constructors
        public DateManager() : this(new DateUtility())
        { }

        public DateManager(IDateUtility dateUtility)
        {
            _dateUtility = dateUtility;
        }
        #endregion

        // Array holding the days in each month
        int[] daysInMonth = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// Business method to calculate days difference between dates
        /// </summary>
        /// <param name="datesModel">Model holding From and To date</param>
        /// <returns>Days count</returns>
        public int CalculateDateDifference(model.DateRequestModel datesModel)
        {
            try
            {
                // Get total days for From and To date
                int fromDateDayCount = _dateUtility.GetDaysCountTillGivenDate(datesModel.FromDate);
                int toDateDayCount = _dateUtility.GetDaysCountTillGivenDate(datesModel.ToDate);

                // Subtract ToDate total days count from FromDate days count which will give the difference in dates
                return toDateDayCount - fromDateDayCount != 0 ? Math.Abs(toDateDayCount - fromDateDayCount) - 1 : 0;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
