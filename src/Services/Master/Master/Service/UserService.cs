using EasyCaching.Core;
using GrpcGetDataToWareHouse;
using Infrastructure;
using Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Share.Base.Service.Caching;
using Share.Base.Service.Caching.CacheName;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaseId = GrpcGetDataToWareHouse.BaseId;

namespace Master.Service
{
    //  [Authorize]
    public class UserService : IUserService
    {
        private readonly MasterdataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IPaginatedList<UserMaster> _list;
        private readonly GrpcGetDataWareHouse.GrpcGetDataWareHouseClient _client;
        private readonly IHybridCachingManager _cacheExtension;


        public UserMaster User => GetUser();

        public UserService(GrpcGetDataWareHouse.GrpcGetDataWareHouseClient client, IPaginatedList<UserMaster> list, MasterdataContext context, IConfiguration configuration, IHttpContextAccessor httpContext, IHybridCachingManager cacheExtension)
        {
            _context = context;
            _configuration = configuration;
            _httpContext = httpContext;
            _list = list;
            _client = client;
            _cacheExtension = cacheExtension;
        }

        private async Task<bool> ValidateAdmin(string username, string password)
        {
            var admin = await _context.UserMasters.AsNoTracking().FirstOrDefaultAsync(x => x.UserName.Equals(username) && x.InActive == true && !x.OnDelete);
            return admin != null && new PasswordHasher<UserMaster>().VerifyHashedPassword(new UserMaster(), admin.Password, password) == PasswordVerificationResult.Success;
        }
        public async Task<bool> Register(RegisterModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var hashedPassword = new PasswordHasher<UserMaster>().HashPassword(new UserMaster(), model.Password);
            var resCreate = new UserMaster()
            {
                Id = Guid.NewGuid().ToString(),
                Role = "User",
                Password = hashedPassword,
                Create = false,
                Delete = false,
                Edit = false,
                InActive = false,
                OnDelete = false,
                Read = true,
                RoleNumber = 1,
                UserName = model.Username,
                WarehouseId = "",
                ListWarehouseId = ""

            };
            await _context.UserMasters.AddAsync(resCreate);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public bool CheckUser(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }
            var res = _context.UserMasters.AsNoTracking().FirstOrDefault(x => x.UserName.Equals(name) && x.OnDelete == false);
            return res == null;
        }

        public UserMaster GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            }
            var res = _context.UserMasters.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id) && x.OnDelete == false);
            // res.Password = "";
            return res;
        }

        public async Task<bool> UpdateUser(UserMaster user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.UserMasters.Update(user);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }


        private UserMaster GetUser()
        {
            var id = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (id is null)
                return null;
            var res = _context.UserMasters.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id.Value) && x.OnDelete == false && x.InActive == true);
            res.Password = "";
            return res;
        }

        public async Task<IPaginatedList<UserMaster>> GetListUserAsync(int pages, int number, string wareHouseId, string keyWords)
        {
            var query = from u in _context.UserMasters
                        where u.OnDelete == false
                        select new UserMaster()
                        {
                            Id = u.Id,
                            Create = u.Create,
                            Edit = u.Edit,
                            Delete = u.Delete,
                            UserName = u.UserName,
                            InActive = u.InActive,
                            Role = u.Role,
                            RoleNumber = u.RoleNumber,
                            Read = u.Read,
                            WarehouseId = u.WarehouseId,
                            Password = "",
                            ListWarehouseId = u.ListWarehouseId
                        };
            if (!string.IsNullOrEmpty(keyWords))
                query = from u in query
                        where u.UserName.Contains(keyWords) || u.Role.Contains(keyWords)
                        select u;
            if (!string.IsNullOrEmpty(wareHouseId))
            {
                var listId = await _client.GetListWarehouseByIdAsync(new BaseId() { IdWareHouse = wareHouseId });
                query = from u in query
                        where listId.IdWareHouseList.Contains(u.WarehouseId)
                        select u;
            }
            _list.Result = query.Skip(pages * number).Take(number);
            _list.totalCount = await query.CountAsync();
            return _list;
        }

        public async Task<bool> SetRoleToUser(UserMaster model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var user = _context.UserMasters.AsNoTracking().FirstOrDefault(x => x.Id.Equals(model.Id) && x.OnDelete == false);
            if (user == null)
                return false;
            if (!string.IsNullOrEmpty(model.WarehouseId) && !user.WarehouseId.Equals(model.WarehouseId))
            {
                var listId = await _client.GetListWarehouseByIdAsync(new BaseId() { IdWareHouse = model.WarehouseId });
                model.ListWarehouseId = listId.IdWareHouseList;
                model.WarehouseId = listId.IdWareHouseList;
            }

            model.Password = user.Password;
            model.UserName = user.UserName;
            model.Role = SelectListRole.Get().FirstOrDefault(x => x.Value.Equals(model.RoleNumber.ToString())).Text;
            model.OnDelete = false;
            _context.UserMasters.Update(model);
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> CheckActiveUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));
            var user = await _context.UserMasters.FirstOrDefaultAsync(x => x.UserName.Equals(userName) && x.OnDelete == false);
            return user.InActive;
        }

        public UserMaster GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or empty.", nameof(userName));
            }
            var res = _context.UserMasters.AsNoTracking().FirstOrDefault(x => x.UserName.Equals(userName) && x.OnDelete == false);
            // res.Password = "";
            return res;
        }

        public async Task<bool> CheckAuthozireByUserIdAndRoleKey(string userId, string roleKey)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleKey))
            {
                throw new ArgumentException($"'{nameof(userId)}' or {nameof(roleKey)}' cannot be null or empty.", nameof(roleKey));
            }
            // get id role
            var roleId = await _context.ListRoles.FirstOrDefaultAsync(x => x.Key.Equals(roleKey));
            if (roleId is null)
                return false;
            // get listid authorize
            var authozireId = _context.ListAuthozireByListRoles.Where(x => x.ListRoleId.Equals(roleId.Id)).Select(x => x.AuthozireId);
            if (authozireId is not null && await authozireId.AnyAsync())
            {
                // get user with userid and authzireid
                var checkListAuthozire = await _context.ListAuthozireRoleByUsers.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && authozireId.Contains(x.ListAuthozireId));
                if (checkListAuthozire is not null)
                    return true;
            }
            //check with role key
            var checkListRole = await _context.ListRoleByUsers.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.ListRoleId.Equals(roleId.Id));
            return checkListRole is not null;
        }

        public async Task CacheListRole(string userId)
        {

            var slidingExpiration = TimeSpan.FromDays(10);
            List<string> listRole = new List<string>();
            // get list roleid by userid
            List<string> listRoleOne = await _context.ListRoleByUsers.Where(x => x.UserId.Equals(userId)).Select(x => x.ListRoleId).ToListAsync();

            if (listRoleOne.Any())
                listRole.AddRange(listRoleOne);


            // get list authozrireid by userid

            List<string> checkListAuthozire = await _context.ListAuthozireRoleByUsers.Where(x => x.UserId.Equals(userId)).Select(x => x.ListAuthozireId).ToListAsync();
            // get list roleid by authozreid
            if (checkListAuthozire.Any())
            {
                List<string> listRoleTwo = await _context.ListAuthozireByListRoles.Where(x => checkListAuthozire.Contains(x.AuthozireId)).Select(x => x.ListRoleId).ToListAsync();
                if (listRoleTwo.Any())
                    listRole.AddRange(listRoleTwo);
            }
            List<string> res = new List<string>();
            if (listRole.Any())
                res.AddRange(await _context.ListRoles.Where(x => listRole.Contains(x.Id)).Select(x => x.Key).ToListAsync());
            await _cacheExtension.HybridCachingProvider.SetAsync(string.Format(UserListRoleCacheName.UserListRoleCache, userId), res, slidingExpiration);


        }

        public async Task RemoveCacheListRole(string userId)
        {
            await _cacheExtension.HybridCachingProvider.RemoveByPrefixAsync(string.Format(UserListRoleCacheName.UserListRoleCache, userId));
        }


        public async Task RemoveAllCacheListRoleByUser()
        {
            await _cacheExtension.HybridCachingProvider.RemoveByPrefixAsync(UserListRoleCacheName.Prefix);
        }


        public async Task<bool> Login(LoginModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            return await ValidateAdmin(model.Username, model.Password);
        }


        [AllowAnonymous]
        public async Task CacheListRoleInactiveFalse()
        {
            var slidingExpiration = TimeSpan.FromDays(10);
            // get list roleid by userid
            var listRoleFalse = await GetListRoleInactiveFalse();
            if (listRoleFalse.AnyList())
                await _cacheExtension.HybridCachingProvider.SetAsync(string.Format(ListRoleCacheName.UserListRoleCache, false), listRoleFalse, slidingExpiration);
        }


        [AllowAnonymous]
        public async Task<IEnumerable<string>> GetListRoleInactiveFalse()
        {
            List<string> listRoleFalse = new List<string>();
            // get list roleid by userid
            listRoleFalse = await _context.ListRoles.Where(x => !x.InActive).Select(x => x.Key).ToListAsync();

            return listRoleFalse;
        }


        [AllowAnonymous]
        public async Task RemoveCacheListRoleInactiveFalse()
        {
            await _cacheExtension.HybridCachingProvider.RemoveByPrefixAsync(string.Format(ListRoleCacheName.UserListRoleCache, false));
        }
    }
}
