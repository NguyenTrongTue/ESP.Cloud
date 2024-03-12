using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng chứa thông tin khuyến mãi của từng garage
    /// </summary>
    [Table("promo_garages")]
    public class PromoGaragesEntity
    {
        /// <summary>
        /// Khóa chính của bảng 'promo_garages'
        /// </summary>
        [Key]
        public Guid PromoGaragesId { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'garage'
        /// </summary>
        public Guid? GarageId { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'garage_services'
        /// </summary>
        public Guid GarageServicesId { get; set; }

        /// <summary>
        /// Tỉ lệ giảm giá (phần trăm)
        /// </summary>
        public decimal? PercentDiscount { get; set; }

        /// <summary>
        /// Ngày bắt đầu ưu đãi
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc ưu đãi
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
