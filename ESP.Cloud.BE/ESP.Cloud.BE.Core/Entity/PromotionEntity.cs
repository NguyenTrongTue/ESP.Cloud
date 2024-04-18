using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng lưu các bài đăng khuyến mãi
    /// </summary>
    [Table("promotions")]
    public class PromotionEntity
    {
        [Key]
        public Guid promotions_id { get; set; }

        /// <summary>
        /// Hình ảnh
        /// </summary>
        public string image_link { get; set; } = string.Empty;

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string title { get; set; } = string.Empty;

        /// <summary>
        /// Tóm tắt
        /// </summary>
        public string summary { get; set; } = string.Empty;

        /// <summary>
        /// Chi tiết nội dung bài đăng
        /// </summary>
        public string description { get; set; } = string.Empty;

        /// <summary>
        /// Mô tả chi tiết về xe
        /// </summary>
        public DateTime? start_date { get; set; }

        /// <summary>
        /// Mô tả chi tiết về xe
        /// </summary>
        public DateTime? end_date { get; set; }

        /// <summary>
        /// Mô tả chi tiết về xe
        /// </summary>
        public Guid garage_id { get; set; }
    }
}
