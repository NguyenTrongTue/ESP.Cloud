using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Param;
using ESP.Cloud.BE.Core.DL.GarageDL;
using ESP.Cloud.BE.Core.Enum;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service
{
    public class GarageService : IGarageService
    {
        private readonly IGarageDL _garageDL;
        public GarageService(IGarageDL garageDL)
        {
            _garageDL = garageDL;
        }
        public async Task<List<object>> GetGaragesAsync(CoordinatesParam param, string sortBy, List<string>? listServiceNames, string? carType, int openTime, int take, int skip)
        {
            var listServiceName = listServiceNames?.Count > 0
                ? $"({string.Join(" or ", listServiceNames.Select(itemParam => $"gs.service_name = '{itemParam}'"))})"
                : "1 = 1";

            var carTypeFilter = String.IsNullOrEmpty(carType) ? "1 = 1" : $"(c.make = '{carType}')";

            var openTimeFilter = MapTimeOpen(openTime);

            var result = await _garageDL.SearchGarageAsync(param?.latitude, param?.longitude, sortBy, listServiceName, carTypeFilter, openTimeFilter, take, skip);

            return result;
        }


        private string MapTimeOpen(int openTime)
        {
            var openTimeFilter = "1 = 1";
            if (openTime >= 0)
            {
                switch (openTime)
                {
                    case (int)TimeOpenEnum.Now:
                        openTimeFilter = $"g.open_time <= '{DateTime.Now.ToString("HH:mm:ss")}' and g.close_time >= '{DateTime.Now.ToString("HH:mm:ss")}'";
                        break;
                    case (int)TimeOpenEnum.AfterWork:
                        openTimeFilter = $" g.close_time >= '17:30:00'";
                        break;
                    case (int)TimeOpenEnum.BeforeWork:
                        openTimeFilter = $" g.open_time >= '08:00:00'";
                        break;
                    case (int)TimeOpenEnum.AllTime:
                        openTimeFilter = $"g.open_time = '00:00:00'";
                        break;
                }
            }

            return openTimeFilter;
        }


        public async Task<List<object>> GetGarageServicesAsync()
        {
            var result = await _garageDL.GetGarageServicesAsync();

            return result;
        }

        public async Task<List<object>> GetCarsAsync()
        {
            var result = await _garageDL.GetCarsAsync();

            return result;
        }

        public async Task<object?> GetGarageByIdAsync(Guid garage_id)
        {
            var result = await _garageDL.GetGarageByIdAsync(garage_id);

            return result;
        }

        public async Task<List<object>> GetGarageReviewsByIdAsync(GarageReviewsParam param)
        {

            var result = await _garageDL.GetGarageReviewsByIdAsync(param.garageId, param.take, param.skip);

            return result;
        }
    }
}
