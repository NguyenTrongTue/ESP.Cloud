namespace ESP.Cloud.BE.Application.Param
{
    public class LikeOrUnLikeParam
    {

        public Guid car_review_id { get; set; }
        public Guid user_id { get; set; }

        public bool liked { get; set; } = false;
        public string fullname { get; set; } = string.Empty;

        public Guid user_id_of_car_review { get; set; } = Guid.Empty;

    }
}
