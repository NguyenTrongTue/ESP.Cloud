using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESP.Cloud.BE.Core.Model
{
    /// <summary>
    /// Bảng chứa thông tin người dùng
    /// </summary>
    [Table("answers")]
    public class AnswerEntity
    {
        /// <summary>
        /// Id loại xe của câu hỏi
        /// </summary>
        [Key]
        public Guid answers_id { get; set; }
        /// <summary>
        /// id câu hỏi - khóa chính
        /// </summary>

        public Guid questions_id { get; set; }

        /// <summary>
        /// Id người hỏi
        /// </summary>
        public Guid user_id { get; set; }
                /// <summary>
        /// Tiêu đề câu hỏi
        /// </summary>
        public string? questions_title { get; set; } = string.Empty;
        /// <summary>
        /// Là 1 câu hỏi phản hồi
        /// </summary>
        public bool is_reply { get; set; } = false;
        /// <summary>
        /// Nội dung câu trả lời
        /// </summary>
        public string answers_content { get; set; } = string.Empty;
        /// <summary>
        /// Id của câu trả lời được phản hồi
        /// </summary>
        public Guid reply_to_answer_id { get; set; }
        /// <summary>
        /// Thời gian tạo câu hỏi
        /// </summary>
        public DateTime? created_date { get; set; } = DateTime.Now;
                /// <summary>
        /// Tên người hỏi
        /// </summary>
        public string user_name { get; set; } = string.Empty;
    }
}
