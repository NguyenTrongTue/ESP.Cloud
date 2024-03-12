using ESP.Cloud.BE.Core.BaseDL.Repository;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Core.DL.GarageDL
{
    public interface IGarageDL : IBaseRepository<GarageEntity>
    {
        Task<List<object>> SearchGarageAsync(double? latitude, double? longitude, string sort_by, string list_vervice_names, string car_type, string p_open_time, int take, int skip);

        Task<List<object>> GetGarageServicesAsync();
        Task<List<object>> GetCarsAsync();

        Task<object> GetGarageByIdAsync(Guid garage_id);

    }
}
