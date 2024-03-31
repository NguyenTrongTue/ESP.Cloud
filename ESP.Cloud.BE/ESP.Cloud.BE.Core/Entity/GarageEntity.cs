using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng lưu thông tin của garage
    /// </summary>
    [Table("garage")]
    public class GarageEntity
    {
        [Key]
        /// <summary>
        /// Id của garage
        /// </summary>
        public Guid garage_id { get; set; }
        [Required]
        /// <summary>
        /// Tên gara
        /// </summary>
        public string garage_name { get; set; } = string.Empty;
        /// <summary>
        /// Kinh độ
        /// </summary>
        public decimal? latitude { get; set; }
        /// <summary>
        /// Vĩ độ
        /// </summary>
        public decimal? longitude { get; set; }
        /// <summary>
        /// Thời gian mở cửa trong ngày
        /// </summary>
        public string? open_time { get; set; }
        /// <summary>
        /// Thời gian đóng cửa
        /// </summary>
        public string? close_time { get; set; }
        /// <summary>
        /// Tên địa chỉ
        /// </summary>
        public string address { get; set; } = string.Empty;
        /// <summary>
        /// Website của gara
        /// </summary>
        public string garage_website { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string phone { get; set; } = string.Empty;
    }
}
