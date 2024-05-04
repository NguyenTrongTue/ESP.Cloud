namespace ESP.Cloud.BE.Core.Model
{
    public class RatingOverviewResult
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

        /// <summary>
        /// Loại đánh giá
        /// </summary>
        public int rating { get; set; }

        /// <summary>
        /// Tổng số lượt đáng giá trên lọai
        /// </summary>
        public int total_rating { get; set; }

    }
}
