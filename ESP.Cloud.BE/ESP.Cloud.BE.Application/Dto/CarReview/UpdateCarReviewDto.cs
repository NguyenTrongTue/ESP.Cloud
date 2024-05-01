namespace ESP.Cloud.BE.Application.Dto
{
    public class UpdateCarReviewDto
    {
        /// <summary>
        /// Id loại xe của câu hỏi
        /// </summary>
        public Guid cars_id { get; set; }
        /// <summary>
        /// Id người hỏi
        /// </summary>
        public Guid user_id { get; set; }
        /// <summary>
        /// Tên người hỏi
        /// </summary>
        public string user_name { get; set; } = string.Empty;
        /// <summary>
        /// Đánh giá 1 => 5 sao
        /// </summary>
        public int rating { get; set; }
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
        /// <summary>
        /// Tiêu đề câu hỏi
        /// </summary>
        public string car_review_title { get; set; } = string.Empty;
        /// <summary>
        /// Nội dung câu hỏi
        /// </summary>
        public string car_review_content { get; set; } = string.Empty;
        /// <summary>
        /// Thời gian tạo câu hỏi
        /// </summary>
        public DateTime? created_date { get; set; } = DateTime.Now;

    }
}
