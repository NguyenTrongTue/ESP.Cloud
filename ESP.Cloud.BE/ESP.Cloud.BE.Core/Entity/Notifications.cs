using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ESP.Cloud.BE.Model
{
    /// <summary>
    /// Bảng lưu thông báo của hệ thống đến người dùng
    /// </summary>
    [Table("user_notifications")]
    public class Notifications
    {
        /// <summary>
        /// Khóa chính của bảng 'user_notifications'
        /// </summary>
        [Key]
        public Guid user_notifications_id { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'user'
        /// </summary>
        public Guid? user_id { get; set; } = Guid.Empty;
        /// <summary>
        /// Nội dung thông báo
        /// 0: thông báo đến lịch sửa xe
        /// 1: thông báo chương trình khuyến mại
        /// </summary>
        public int? type { get; set; } = 0;

        /// <summary>
        /// Mô tả nội dung thông báo 
        /// </summary>
        public string? description { get; set; } = string.Empty;

        /// <summary>
        /// Cờ kiểm tra người dùng đã đọc message chưa.
        /// </summary>
        public bool? unread { get; set; } = true;
        /// <summary>
        /// Thời gian đặt lịch
        /// </summary>
        public DateTime created_time { get; set; } = DateTime.Now;
    }
}

