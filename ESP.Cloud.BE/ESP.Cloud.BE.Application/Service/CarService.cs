using AutoMapper;
using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Service.Base;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Application.Service
{
    public class CarService : BaseReadService<CarsEntity>, ICarService
    {
        private readonly ICarDL _carDL;
        public CarService(ICarDL carDL, IMapper mapper) : base(carDL, mapper)
        {
            _carDL = carDL;
        }
    }
}
