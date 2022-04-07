using Infrastructure;
using Master.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Master.Service
{
    //  [Authorize]
    public class UserService : IUserService
    {
        private readonly MasterdataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;

        public UserMaster User => GetUser();

        public UserService(MasterdataContext context, IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _context = context;
            _configuration = configuration;
            _httpContext = httpContext;
        }


        public string GenerateJWT(LoginModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (ValidateAdmin(model.Username, model.Password))
            {
                var user = _context.UserMasters.AsNoTracking().FirstOrDefault(x => x.UserName.Equals(model.Username) && x.InActive == true && !x.OnDelete);
                var authClaims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.UserName),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                //new Claim(ClaimTypes.Role, user.Role),
                                //new Claim("Create", user.Create),
                                //new Claim("Edit", user.Edit),
                                //new Claim("Delete", user.Delete),
                                //new Claim("Read", user.Read),
                              
                                new Claim("id",user.Id)
                            };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: model.Remember ? DateTime.Now.AddYears(1) : DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
                return string.Empty;

        }
        private bool ValidateAdmin(string username, string password)
        {
            var admin = _context.UserMasters.AsNoTracking().FirstOrDefault(x => x.UserName.Equals(username) && x.InActive == true && !x.OnDelete);
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
                InActive = true,
                OnDelete = false,
                Read = true,
                RoleNumber = 1,
                UserName = model.Username,
                WarehouseId = ""

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
            res.Password = "";
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
            var res = _context.UserMasters.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id.Value) && x.OnDelete == false);
            res.Password = "";
            return res;
        }
    }
}
