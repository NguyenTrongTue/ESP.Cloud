namespace ESP.Cloud.BE.Application.Dto.CarReview
{
    public class CarReviewOverviewRatingDto
    {
        /// <summary>
        /// Id loại xe của câu hỏi
        /// </summary>
        public Guid cars_id { get; set; }

        /// <summary>
        /// Tên loại xe - lưu dư thừa
        /// </summary>
        public string make { get; set; } = string.Empty;
        /// <summary>
        /// Năm sản xuất - lưu dư thừa
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// Mô hình loại xe  - lưu dư thừa
        /// </summary>
        public string model { get; set; } = string.Empty;


        public int five_stars { get; set; } = 0;
        public int four_stars { get; set; } = 0;
        public int three_stars { get; set; } = 0;
        public int two_stars { get; set; } = 0;
        public int one_stars { get; set; } = 0;


        public int total_rating { get; set; } = 0;
        public double avg_rating { get; set; } = 0;

    }
}
