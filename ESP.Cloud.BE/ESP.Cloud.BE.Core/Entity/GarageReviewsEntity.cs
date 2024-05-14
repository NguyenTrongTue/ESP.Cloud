using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng lưu phản hồi của nguời dùng về garage
    /// </summary>
    [Table("garage_reviews")]
    public class GarageReviewsEntity
    {
        /// <summary>
        /// Khóa chính của bảng 'garage_reviews'
        /// </summary>
        [Key]
        public Guid garage_reviews_id { get; set; }

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
