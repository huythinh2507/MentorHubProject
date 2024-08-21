namespace YPP.MH.BusinessLogicLayer.Util
{
    public class CustomDateTime
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime PreFromDate { get; set; }
        public TimeSpan TimeDiff { get; set; }

        public CustomDateTime(string? fromDate, string? toDate)
        {
            FromDate = string.IsNullOrEmpty(fromDate) ? new DateTime(1970, 1, 1) : DateTime.Parse(fromDate).Date;
            ToDate = string.IsNullOrEmpty(toDate) ? DateTime.Now.AddDays(1).Date : DateTime.Parse(toDate).AddDays(1).Date;
            TimeDiff = ToDate.Date - FromDate.Date;
            PreFromDate = string.IsNullOrEmpty(fromDate) ? FromDate : FromDate.AddDays(-TimeDiff.Days - 1).Date;
        }
    }
}
