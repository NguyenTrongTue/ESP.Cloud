﻿using ESP.Cloud.BE.Core.BaseDL.Repository;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Core.DL
{
    public interface IQuestionsDL : IBaseRepository<QuestionsEntity>
    {
        /// <summary>
        /// Hàm lấy các câu hỏi phổ biến
        /// </summary>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        Task<List<object>> GetQuestionPopular();
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo id của xe
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        Task<List<object>> GetQuestionByCarAsync(Guid carId);

        /// <summary>
        /// Hàm tìm kiếm câu hỏi theo tiêu đề câu hỏi
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        Task<List<object>> SearchQuestionByTitleAsync(string title);
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo make
        /// </summary>
        /// <param name="make"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        Task<List<object>> GetQuestionByMakeAsync(string make);
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo year
        /// </summary>
        /// <param name="make"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        Task<List<object>> GetQuestionByYearAsync(string make, int year);
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo model
        /// </summary>
        /// <param name="make"></param>
        /// <param name="year"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        Task<List<object>> GetQuestionByModelAsync(string make, int year, string model);

    }
}
