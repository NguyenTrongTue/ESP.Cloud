using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng lưu lịch sử đặt xe của người dùng.
    /// </summary>
    [Table("garage_service")]
    public class GarageServicesEntity
    {
        /// <summary>
        /// Khóa chính của bảng 'garage_services'
        /// </summary>
        [Key]
        public Guid GarageServicesId { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'garage'
        /// </summary>
        public Guid? GarageId { get; set; }

        /// <summary>
        /// Mã dịch vụ
        /// </summary>
        public string ServiceCode { get; set; } = string.Empty;

        /// <summary>
        /// Tên dịch vụ
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;

        /// <summary>
        /// Giá dịch vụ
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Mô tả chi tiết về dịch vụ
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Thời gian thực hiện dịch vụ (đơn vị: phút)
        /// </summary>
        public int? Duration { get; set; }
    }
}
