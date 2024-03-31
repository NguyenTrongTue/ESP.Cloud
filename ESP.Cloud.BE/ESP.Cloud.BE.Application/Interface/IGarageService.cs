using ESP.Cloud.BE.Application.Param;
using ESP.Cloud.BE.Core.Model;
using System.Threading.Tasks;

namespace ESP.Cloud.BE.Application.Interface
{
    public interface IGarageService
    {
        Task<List<object>> GetGaragesAsync(CoordinatesParam? param, string sortBy, List<string>? listServiceNames, string? carType, int openTime, int take, int skip);

        Task<List<object>> GetGarageServicesAsync();

        Task<List<object>> GetCarsAsync();

        Task<object?> GetGarageByIdAsync(Guid garage_id);


        Task<List<object>> GetGarageReviewsByIdAsync(GarageReviewsParam param);
    }
}
