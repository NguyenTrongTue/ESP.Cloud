using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class CarDL : BaseReadRepository<CarsEntity>, ICarDL
    {
        #region Fields
        protected readonly IUnitOfWork _uow;
        #endregion

        #region Constructor
        public CarDL(IUnitOfWork entityOfWork) : base(entityOfWork)
        {
            _uow = entityOfWork;
        }
        #endregion
    }
}
