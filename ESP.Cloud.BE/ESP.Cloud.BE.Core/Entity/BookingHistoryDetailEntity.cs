using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng lưu lịch sử chi tiết đặt xe của người dùng.
    /// </summary>
    [Table("booking_history_detail")]    
    public class BookingHistoryDetailEntity
    {
        /// <summary>
        /// Khóa chính của bảng 'booking_history_detail'
        /// </summary>
        [Key]
        public Guid BookingHistoryDetailId { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'booking_history'
        /// </summary>
        public Guid? BookingHistoryId { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'garage_services'
        /// </summary>
        public Guid? GarageServicesId { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal? TotalAmount { get; set; }
    }
}
