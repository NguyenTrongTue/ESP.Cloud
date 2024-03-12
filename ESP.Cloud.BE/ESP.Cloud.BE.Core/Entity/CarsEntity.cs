using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng lưu các mẫu xe hiện có
    /// </summary>
    [Table("cars")]
    public class CarsEntity
    {
        [Key]
        public Guid cars_id { get; set; }
        /// <summary>
        /// Nhãn hiệu của xe (Ví dụ: Toyota, Honda, ...)
        /// </summary>
        public string make { get; set; } = string.Empty;
        /// <summary>
        /// Mô hình của xe (Ví dụ: Camry, Accord, ...)
        /// </summary>
        public string model { get; set; } = string.Empty;
        /// <summary>
        /// Năm sản xuất
        /// </summary>
        public int? year { get; set; }
        /// <summary>
        /// Số chỗ
        /// </summary>
        public int? seats { get; set; }
        /// <summary>
        /// Mô tả chi tiết về xe
        /// </summary>
        public string description { get; set; } = string.Empty;
    }
}
