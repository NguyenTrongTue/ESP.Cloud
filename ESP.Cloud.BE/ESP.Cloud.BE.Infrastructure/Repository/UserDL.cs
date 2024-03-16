using Dapper;
using ESP.Cloud.BE.Core.BaseDL;
using ESP.Cloud.BE.Core.DL.UserDL;
using ESP.Cloud.BE.Core.Model;
using ESP.Cloud.BE.Infrastructure.Repository.Base;
using ESP.Cloud.BE.Infrastructure;

namespace ESP.Cloud.BE.Infrastructure.Repository
{
    public class UserDL : BaseRepository<UserEntity>, IUserDL
    {
        #region Fields
        protected readonly IUnitOfWork _uow;
        #endregion

        #region Constructor
        public UserDL(IUnitOfWork entityOfWork) : base(entityOfWork)
        {
            _uow = entityOfWork;
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            var param = new Dictionary<string, object>
                {
                    { "email", email }
                };

            var sql = $"select * from public.user where email = @email";

            var entity = await _uow.QueryDefault<UserEntity>(sql, param);

            return entity;
        }

        public async Task UpdatePassWordAsync(byte[] passwordHash, byte[] passwordSalt, Guid user_id)
        {
            var param = new Dictionary<string, object>
                {
                    { "password_hash", passwordHash },
                    { "password_salt", passwordSalt },
                    { "user_id", user_id }
                };

            var dynamicParam = new DynamicParameters(param);

            var sql = $"update public.\"user\" \r\nset password_hash  = @password_hash, password_salt = @password_salt \r\nwhere user_id = @user_id;";

            var entity = await _uow.Connection.ExecuteAsync(sql, dynamicParam);
        }

        #endregion
    }
}
