using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng lưu lịch sử đặt xe của người dùng.
    /// </summary>
    [Table("booking_history")]
    public class BookingHistoryEntity
    {
        /// <summary>
        /// Khóa chính của bảng 'booking_history'
        /// </summary>
        [Key]
        public Guid booking_history_id { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'garage'
        /// </summary>
        public Guid? garage_id { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'user'
        /// </summary>
        public Guid? user_id { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'cars'
        /// </summary>
        public Guid? cars_id { get; set; }

        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? comment { get; set; } = string.Empty;

        /// <summary>
        /// Ngày đặt chỗ
        /// </summary>
        public DateTime booking_date { get; set; }
        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? first_name { get; set; } = string.Empty;
        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? last_name { get; set; } = string.Empty;
        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string email { get; set; } = string.Empty;
        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? phone { get; set; } = string.Empty;

        /// <summary>
        /// Thời gian đặt lịch
        /// </summary>
        public DateTime created_date { get; set; } = DateTime.Now;
    }
}
