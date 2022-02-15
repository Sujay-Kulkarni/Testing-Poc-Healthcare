using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.DBContexts;
using Testing_Poc_Healthcare.Interface;
using Testing_Poc_Healthcare.Models;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace Testing_Poc_Healthcare.Services
{
    public class UserService : IUserService
    {
        private readonly HealthCareDBContext _dbContext;        
        public UserService(HealthCareDBContext healthCareDbContext)
        {
            _dbContext = healthCareDbContext;
        }

        public bool AddUserDetails(UserInfo user)
        {
            var existingRecord = _dbContext.UserInfos.Where(e => e.FirstName == user.FirstName
            && e.LastName == user.LastName && e.EmailId == user.EmailId).FirstOrDefault();

            if (existingRecord == null)
            {
                user.CreatDate = DateTime.Now;
                user.CreatedBy = "Admin";
                _dbContext.UserInfos.Add(user);
                return _dbContext.SaveChanges() > 0;
            }

            return false;
        }

        public UserInfoVM GetUserDetails(UserLogin userLogin, JwtInfo jwtInfo)
        {
            var userDetails = _dbContext.UserInfos.Where(u => u.EmailId == userLogin.EmailId && u.Password == userLogin.Password).FirstOrDefault();

            if (userDetails != null)
            {
                var tokenkey = GenerateToken((
                    new UserInfoVM {
                        EmailId = userDetails.EmailId,
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        Gender = (userDetails.Gender) ? "Male" : "Female",
                        LastLogin = userDetails.LastLogin,
                        RoleName = (userDetails.Roles != null) ? userDetails.Roles.FirstOrDefault().RoleName : string.Empty,UserId = userDetails.UserId}), jwtInfo) ;

                return new UserInfoVM
                {
                    EmailId = userDetails.EmailId,
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Gender =  (userDetails.Gender)  ? "Male" : "Female",
                    LastLogin = userDetails.LastLogin,
                    RoleName = (userDetails.Roles != null) ? userDetails.Roles.FirstOrDefault().RoleName : string.Empty,
                    UserId = userDetails.UserId
                };
            } else { return null; }
        }

        public string GenerateToken(UserInfoVM userDetails, JwtInfo jwtInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.Jwtkey));//_config["Jwt:Key"])
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
             {
                new Claim(ClaimTypes.NameIdentifier, userDetails.EmailId),
                new Claim(ClaimTypes.Email, userDetails.EmailId),
                new Claim(ClaimTypes.GivenName, userDetails.FirstName),
                new Claim(ClaimTypes.Surname, userDetails.LastName),
                new Claim(ClaimTypes.Role, userDetails.RoleName)
            };

            var token = new JwtSecurityToken(jwtInfo.Issuer,
              jwtInfo.Audience,
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

            //private int UpdateLastLogi(UserInfo userInfo)
            //{ 
            //    var 
            //}
        }
}
