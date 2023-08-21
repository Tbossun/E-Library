using AutoMapper;
using E_Library.Core.Services.Interfaces;
using E_Library.Data.DTOS.Response;
using E_Library.Data.Models;
using E_Library.Data.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Core.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<ReviewDto>> AddReviewAsync(AddReviewDto reviewDto)
        {
            try
            {
                var review = _mapper.Map<Reviews>(reviewDto);
                _unitOfWork.ReviewsRepo.Add(review);
                 _unitOfWork.Save();

                var createdReviewDto = _mapper.Map<ReviewDto>(review);

                var response = new ResponseDto<ReviewDto>
                {
                    StatusCode = (int)HttpStatusCode.Created,
                    DisplayMessage = "Review added successfully.",
                    Result = createdReviewDto
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<ReviewDto>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = null
                };

                return response;
            }
        }

        public async Task<ResponseDto<bool>> DeleteReviewAsync(string reviewId)
        {
            try
            {
                var existingReview =  _unitOfWork.ReviewsRepo.Get(r => r.Id == reviewId);

                if (existingReview == null)
                {
                    var notFoundResponse = new ResponseDto<bool>
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        DisplayMessage = "Review not found.",
                        Result = false
                    };

                    return notFoundResponse;
                }

                _unitOfWork.ReviewsRepo.Remove(existingReview);
                _unitOfWork.Save();

                var response = new ResponseDto<bool>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    DisplayMessage = "Review deleted successfully.",
                    Result = true
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<bool>
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    DisplayMessage = $"An error occurred: {ex.Message}",
                    Result = false
                };

                return response;
            }
        }

        public Task<ResponseDto<ReviewDto>> UpdateReviewAsync(string reviewId, UpdateReviewDto reviewDto)
        {
            throw new NotImplementedException();
        }
    }
}
