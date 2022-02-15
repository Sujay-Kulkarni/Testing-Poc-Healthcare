using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.DBContexts;
using Testing_Poc_Healthcare.Interface;
using Testing_Poc_Healthcare.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using log4net;
using Newtonsoft.Json;

namespace Testing_Poc_Healthcare.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly HealthCareDBContext _DBContext;
        private readonly IUserService _userService;
        private IConfiguration _configuration;
        public string jwtkey = "";
        public string issuer ="";
        public string audience = "";
        private readonly ILog logger;

        public AccountController(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;

            jwtkey = configuration.GetSection("Jwt").GetSection("Key").Value;
            issuer = configuration.GetSection("Jwt").GetSection("Issuer").Value;
            audience = configuration.GetSection("Jwt").GetSection("Audience").Value;
            logger = LogManager.GetLogger(typeof(AccountController));

    }

        [AllowAnonymous]
        [HttpPost("Login")]        
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            logger.Info("Login method called");
            //logger.Info(JsonConvert.SerializeObject(userLogin));

            JwtInfo jwtInfo = new JwtInfo();
            try
            {
                jwtInfo.Jwtkey = jwtkey;
                jwtInfo.Issuer = issuer;
                jwtInfo.Audience = audience;

                var response = _userService.GetUserDetails(userLogin, jwtInfo);
                if (response != null)
                {
                    logger.Info("Login successfull");
                    return Ok(response);
                }
                else
                {
                    logger.Error("Incorrect User name or Password");
                    return BadRequest("Incorrect User name or Password");
                }
                
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest("Somethinng Went Wrong!");
            }
        }
        
        [HttpPost("AddUser")]
        public ActionResult AddUser(UserInfo user)
        {
            logger.Info("AddUser method called");
            logger.Info(JsonConvert.SerializeObject(user));
            try
            {
                var response = _userService.AddUserDetails(user);

                if (response)
                {
                    logger.Info("User created successfully " + JsonConvert.SerializeObject(response));
                    return Ok("User created successfully");
                }

                logger.Info("User already exists");
                return NotFound("User already exists");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest("Somethinng Went Wrong!");
            }
        }

        [HttpPost("AddRole")]
        public ActionResult AddRole(Role role)
        {
            logger.Info("AddRole method called ");
            logger.Info(JsonConvert.SerializeObject(role));
            try
            {
                if (role.RoleId > 0)
                {
                    role.RoleId = 0;
                }
                var response = _userService.AddRole(role);
                if (response)
                {
                    logger.Info("Role added successfully" + JsonConvert.SerializeObject(response));
                    return Ok("Role added successfully");
                }

                logger.Info("Role already exists");
                return BadRequest("Role already exists");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest("Somethinng Went Wrong!");
            }
        }
    }
}
