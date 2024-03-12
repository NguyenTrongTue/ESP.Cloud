namespace ESP.Cloud.BE.Application.Param
{
    public class SearchGarageParam
    {

        public CoordinatesParam Coordinates { get; set; }
        public string SortBy { get; set; } 
        public List<string>? ListServiceNames { get; set; }
        public string? CarType { get; set; }
        public int TimeOpen { get; set; }
        public int take { get; set; }
        public int skip { get; set; }
    }

   
}
