using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class ReviewDL : BaseRepository<GarageReviewsEntity>, IReviewDL
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReviewDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
