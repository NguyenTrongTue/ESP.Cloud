﻿using ESP.Cloud.BE.Application.Dto;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using ESP.Cloud.BE.Application.Service;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Host.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarReviewController : BaseController<CarReviewEntity, CreateCarReviewDto, UpdateCarReviewDto>
    {
        private readonly ICarReviewService _carReviewService;
        public CarReviewController(ICarReviewService carReviewService) : base(carReviewService)
        {
            _carReviewService = carReviewService;
        }


        [HttpPost("like_or_unlike")]
        public async Task<IActionResult> LikeOrUnLikeAsync(LikeOrUnLikeParam param)
        {
            try
            {
                await _carReviewService.LikeOrUnLikeAsync(param);

                return Ok(1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo id của xe
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_overview_rating")]
        public async Task<IActionResult> GetOverviewRating(string? make = "")
        {
            try
            {
                var results = await _carReviewService.GetOverviewRatingAsync(make);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo id của xe
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_popular")]
        public async Task<IActionResult> GetQuestionPopular(Guid userId)
        {
            try
            {
                var results = await _carReviewService.GetQuestionPopular(userId);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo id của xe
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_by_car")]
        public async Task<IActionResult> GetQuestionByCar(Guid carId)
        {
            try
            {
                var results = await _carReviewService.GetQuestionByCarAsync(carId);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Hàm tìm kiếm câu hỏi theo tiêu đề câu hỏi
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("search_questions_by_title")]
        public async Task<IActionResult> SearchQuestionByTitle(string title)
        {
            try
            {
                var results = await _carReviewService.SearchQuestionByTitleAsync(title);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo make
        /// </summary>
        /// <param name="make"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_by_make")]
        public async Task<IActionResult> GetQuestionByMake(string make, Guid userId)
        {
            try
            {
                var results = await _carReviewService.GetQuestionByMakeAsync(userId, make);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Hàm lấy thông tin câu hỏi theo model
        /// </summary>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created by: nttue 07/04/2024
        [HttpGet("get_questions_by_model")]
        public async Task<IActionResult> GetQuestionByModel(string make, string model, Guid userId)
        {
            try
            {
                var results = await _carReviewService.GetQuestionByModelAsync(make, model, userId);

                return Ok(results);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}