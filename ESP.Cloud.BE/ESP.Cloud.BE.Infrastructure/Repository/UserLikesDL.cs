using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;
using Newtonsoft.Json;
using System.Data;
using static Dapper.SqlMapper;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class UserLikesDL : BaseRepository<UserLikesEntity>, IUserLikesDL
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserLikesDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeteleUserLikes(Guid carReviewId, Guid userId)
        {



            var param = new Dictionary<string, object>
                {
                    { $"@carReviewId", carReviewId },
                    { $"@userId", userId },
                };

            var sql = $"delete from public.user_likes where car_review_id = @carReviewId and user_id = @userId";

            await _uow.ExecuteDefault(sql, param);
        }
    }
}
