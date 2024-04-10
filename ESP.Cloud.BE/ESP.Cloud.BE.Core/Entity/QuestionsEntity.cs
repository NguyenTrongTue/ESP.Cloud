using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng chứa thông tin người dùng
    /// </summary>
    [Table("questions")]
    public class QuestionsEntity
    {
        /// <summary>
        /// id câu hỏi - khóa chính
        /// </summary>
        [Key]
        public Guid questions_id { get; set; }
        /// <summary>
        /// Id loại xe của câu hỏi
        /// </summary>
        public Guid cars_id { get; set; }
        /// <summary>
        /// Id người hỏi
        /// </summary>
        public Guid user_id { get; set; }
        /// <summary>
        /// Tên người hỏi
        /// </summary>
        public string user_name { get; set; } = string.Empty;
        /// <summary>
        /// Tên loại xe - lưu dư thừa
        /// </summary>
        public string make { get; set; } = string.Empty;
        /// <summary>
        /// Năm sản xuất - lưu dư thừa
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// Mô hình loại xe  - lưu dư thừa
        /// </summary>
        public string model { get; set; } = string.Empty;
        /// <summary>
        /// Tiêu đề câu hỏi
        /// </summary>
        public string questions_title { get; set; } = string.Empty;
        /// <summary>
        /// Nội dung câu hỏi
        /// </summary>
        public string question_content { get; set; } = string.Empty;
        /// <summary>
        /// Thời gian tạo câu hỏi
        /// </summary>
        public DateTime? created_date { get; set; } = DateTime.Now;
    }
}
