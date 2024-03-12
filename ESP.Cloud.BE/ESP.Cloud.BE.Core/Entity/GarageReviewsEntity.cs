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
        public Guid GarageReviewsId { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'garage'
        /// </summary>
        public Guid? GarageId { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'user'
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Bình luận về gara
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// Điểm đánh giá
        /// </summary>
        public int? Rating { get; set; } = 0;

        /// <summary>
        /// Đường dẫn đến hình ảnh
        /// </summary>
        public string Image { get; set; } = string.Empty;
    }
}
