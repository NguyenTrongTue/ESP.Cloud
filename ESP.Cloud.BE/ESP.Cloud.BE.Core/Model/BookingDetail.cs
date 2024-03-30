namespace ESP.Cloud.BE.Core.Model
{
    public class BookingDetail : BookingHistoryEntity
    {

        public string make { get; set; } = string.Empty;
        public string model { get; set; } = string.Empty;
        public int year { get; set; } = 0;
        public string garage_name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
