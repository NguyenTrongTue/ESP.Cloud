﻿namespace ESP.Cloud.BE.Application.Dto
{
    public class UpdateBookingDto
    {
        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'garage'
        /// </summary>
        public Guid? garage_id { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'user'
        /// </summary>
        public Guid? user_id { get; set; }

        /// <summary>
        /// Khóa ngoại trỏ đến bảng 'cars'
        /// </summary>
        public Guid? cars_id { get; set; }

        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? comment { get; set; } = string.Empty;

        /// <summary>
        /// Ngày đặt chỗ
        /// </summary>
        public DateTime? booking_date { get; set; }
        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? first_name { get; set; } = string.Empty;
        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? last_name { get; set; } = string.Empty;
        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? email { get; set; } = string.Empty;
        /// <summary>
        /// Bình luận về đặt chỗ
        /// </summary>
        public string? phone { get; set; } = string.Empty;

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

    }
}
