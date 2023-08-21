using E_Library.Data.DTOS.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Core.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ResponseDto<ReviewDto>> AddReviewAsync(AddReviewDto reviewDto);
        Task<ResponseDto<ReviewDto>> UpdateReviewAsync(string reviewId, UpdateReviewDto reviewDto);
        Task<ResponseDto<bool>> DeleteReviewAsync(string reviewId);
    }
}
