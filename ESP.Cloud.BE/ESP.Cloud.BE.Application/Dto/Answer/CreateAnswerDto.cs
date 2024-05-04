namespace ESP.Cloud.BE.Application.Dto
{
    public class CreateAnswerDto
    {
        /// <summary>
        /// id câu hỏi - khóa chính
        /// </summary>

        public Guid questions_id { get; set; }

        /// <summary>
        /// Id ngưởi trả lời 
        /// </summary>
        public Guid user_id { get; set; }
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
        public Guid? reply_to_answer_id { get; set; }
        /// <summary>
        /// Tiêu đề câu hỏi
        /// </summary>
        public string? questions_title { get; set; } = string.Empty;
        /// <summary>
        /// Tên người hỏi
        /// </summary>
        public string user_name { get; set; } = string.Empty;

        /// <summary>
        /// Id của người đặt câu hỏi
        /// </summary>
        public Guid user_id_of_question { get; set; }
    }
}
