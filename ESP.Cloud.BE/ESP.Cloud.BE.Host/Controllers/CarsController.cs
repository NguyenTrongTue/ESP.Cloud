using ESP.Cloud.BE.Application.Interface;
using ESP.Cloud.BE.Application.Interface.Base;
using ESP.Cloud.BE.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace ESP.Cloud.BE.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : BaseReadControler<CarsEntity>
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService) : base(carService)
        {
            _carService = carService;
        }
    }
}
