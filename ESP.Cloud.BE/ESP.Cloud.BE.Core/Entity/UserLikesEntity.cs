using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng chứa thông tin người dùng
    /// </summary>
    [Table("user_likes")]
    public class UserLikesEntity
    {
        [Key]
        public Guid user_likes_id { get; set; }


        public Guid car_review_id { get; set; }
        public Guid user_id { get; set; }


        public DateTime? created_time { get; set; } = DateTime.Now;
    }
}
