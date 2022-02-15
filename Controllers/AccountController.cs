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
        public string Audience = "";

        public AccountController(IUserService userService,IConfiguration configuration)
        {
           // _DBContext = healthCareDBContext;
            _userService = userService;
            _configuration = configuration;

            jwtkey = configuration.GetSection("Jwt").GetSection("Key").Value;
            issuer = configuration.GetSection("Jwt").GetSection("Issuer").Value;
            Audience = configuration.GetSection("Jwt").GetSection("Audience").Value;

    }

        [AllowAnonymous]
        [HttpPost("Login")]        
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            JwtInfo jwtInfo = new JwtInfo();
            jwtInfo.Jwtkey = jwtkey;
            jwtInfo.Issuer = issuer;
            jwtInfo.Audience = Audience;

            var response = _userService.GetUserDetails(userLogin, jwtInfo);
            if(response != null)
            {
                return Ok(response);
            } else
            {
                return BadRequest("Incorrect User name or Password");
            }
        }
        
        [HttpPost("AddUser")]
        public ActionResult AddUser(UserInfo user)
        {
            var response = _userService.AddUserDetails(user);

            if(response)
            {
                return Ok("User created successfully");
            }

            return NotFound("User already exists");
        }

        [HttpPost("AddRole")]
        public ActionResult AddRole(Role role)
        {
            if (role.RoleId > 0)
            {
                role.RoleId = 0;
            }
            var response = _userService.AddRole(role);
            if (response)
            {
              return Ok("Role added successfully");
            }

            return BadRequest("Role already exists");
        }
    }
}
