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
using log4net;
using Newtonsoft.Json;

namespace Testing_Poc_Healthcare.Services
{
    public class UserService : IUserService
    {
        private readonly HealthCareDBContext _dbContext;
        public ILog logger;
        public UserService(HealthCareDBContext healthCareDbContext)
        {
            _dbContext = healthCareDbContext;
            logger = LogManager.GetLogger(typeof(UserService));
        }

        public bool AddRole(Role role)
        {
            try
            {
                logger.Info("Method Call AddRole");
                logger.Info(JsonConvert.SerializeObject(role));
                var isRoleExists = _dbContext.Roles.Where(r => r.RoleName.ToLower() ==
                role.RoleName.ToLower()).Count() > 0;
                if (!isRoleExists)
                {
                    role.IsActive = true;
                    _dbContext.Roles.Add(role);
                    logger.Info("Role Added " + JsonConvert.SerializeObject(role));
                    return _dbContext.SaveChanges() > 0;
                }
                logger.Info("Role doesn't exist!");
                return false;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw ex;
            }
        }

        public bool AddUserDetails(UserInfo user)
        {
            try
            {
                logger.Info("Method Call AddUserDetails");
                logger.Info(JsonConvert.SerializeObject(user));

                var existingRecord = _dbContext.UserInfos.Where(e => e.FirstName == user.FirstName
                && e.LastName == user.LastName && e.EmailId == user.EmailId).FirstOrDefault();

                if (existingRecord == null)
                {
                    user.CreatDate = DateTime.Now;
                    user.CreatedBy = "Admin";
                    _dbContext.UserInfos.Add(user);
                    _dbContext.SaveChanges();

                    logger.Info("user added " + JsonConvert.SerializeObject(user));
                    return AssignUserRole(user.UserId, 1);
                }
                logger.Info("user already exist " + JsonConvert.SerializeObject(existingRecord));
                return false;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw ex;
            }
        }

        public UserInfoVM GetUserDetails(UserLogin userLogin, JwtInfo jwtInfo)
        {
            try
            {
                logger.Info("Method Call GetUserDetails");
                //logger.Info(JsonConvert.SerializeObject(userLogin));

                var userDetails = _dbContext.UserInfos.Where(u => u.EmailId == userLogin.EmailId && u.Password == userLogin.Password).FirstOrDefault();

                if (userDetails != null)
                {
                    var tokenkey = GenerateToken((
                        new UserInfoVM
                        {
                            EmailId = userDetails.EmailId,
                            FirstName = userDetails.FirstName,
                            LastName = userDetails.LastName,
                            Gender = (userDetails.Gender) ? "Male" : "Female",
                            LastLogin = userDetails.LastLogin,
                            UserId = userDetails.UserId
                        }), jwtInfo);
                    logger.Info("user details " + JsonConvert.SerializeObject(userDetails));
                    return new UserInfoVM
                    {
                        EmailId = userDetails.EmailId,
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        Gender = (userDetails.Gender) ? "Male" : "Female",
                        LastLogin = userDetails.LastLogin,
                        RoleName = "0",
                        UserId = userDetails.UserId,
                        Token = tokenkey

                    };

                }
                else
                {
                    logger.Info("user does not exist " + JsonConvert.SerializeObject(userLogin));
                    return null;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;

            }

        }

        public string GenerateToken(UserInfoVM userDetails, JwtInfo jwtInfo)
        {
            logger.Info("Method Call GenerateToken");
            
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.Jwtkey));//_config["Jwt:Key"])
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                 {
                new Claim(ClaimTypes.NameIdentifier, userDetails.EmailId),
                new Claim(ClaimTypes.Email, userDetails.EmailId),
                new Claim(ClaimTypes.GivenName, userDetails.FirstName),
                new Claim(ClaimTypes.Surname, userDetails.LastName)
               // new Claim(ClaimTypes.Role, userDetails.RoleName)
            };

                var token = new JwtSecurityToken(jwtInfo.Issuer,
                  jwtInfo.Audience,
                  claims,
                  expires: DateTime.Now.AddMinutes(15),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;

            }
        }

    
    private bool AssignUserRole(int userId, int roleId)
        {
            logger.Info("Method Call AssignUserRole");
            try
            {
                if (userId > 0 && roleId > 0)
                {
                    _dbContext.UserRoles.Add(new UserRoles { UserId = userId, RoleId = roleId });
                    logger.Info("Role Created");
                    return _dbContext.SaveChanges() > 0;
                }

                return false;
            }
            catch (Exception ex) 
            {
                logger.Error(ex.ToString());
                throw ex;
            }
        }
    }
}
