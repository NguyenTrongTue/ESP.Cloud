namespace ESP.Cloud.BE.Application.Dto
{
    public class UpdateReviewDto
    {
        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'garage'
        /// </summary>
        public Guid? garage_id { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'user'
        /// </summary>
        public Guid? user_id { get; set; }

        /// <summary>
        /// Bình luận về gara
        /// </summary>
        public string comment { get; set; } = string.Empty;

        /// <summary>
        /// Điểm đánh giá
        /// </summary>
        public int? rating { get; set; } = 0;

        /// <summary>
        /// Đường dẫn đến hình ảnh
        /// </summary>
        public string image { get; set; } = string.Empty;
    }
}
