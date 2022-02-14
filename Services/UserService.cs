﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.DBContexts;
using Testing_Poc_Healthcare.Interface;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Services
{
    public class UserService : IUserService
    {
        private readonly HealthCareDBContext _dbContext;
        public UserService(HealthCareDBContext healthCareDbContext)
        {
            _dbContext = healthCareDbContext;
        }

        public bool AddRole(Role role)
        {
            try {
                var isRoleExists = _dbContext.Roles.Where(r => r.RoleName.ToLower() == 
                role.RoleName.ToLower()).Count() > 0;
                if (!isRoleExists)
                {
                    role.IsActive = true;
                    _dbContext.Roles.Add(role);
                    return _dbContext.SaveChanges() > 0;
                }

                return false;

            } catch (Exception ex) {
                throw ex;
            }
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
                _dbContext.SaveChanges();

                return  AssignUserRole(user.UserId, 1);
            }

            return false;
        }

        public UserInfoVM GetUserDetails(UserLogin userLogin)
        {
            var userDetails = _dbContext.UserInfos.Where(u => u.EmailId == userLogin.EmailId && u.Password == userLogin.Password).FirstOrDefault();

            if(userDetails != null)
            {
                return new UserInfoVM
                {
                    EmailId = userDetails.EmailId,
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Gender =  (userDetails.Gender)  ? "Male" : "Female",
                    LastLogin = userDetails.LastLogin,
                    RoleName = "0",
                    UserId = userDetails.UserId
                };
            } else { return null; }
        }

        //private int UpdateLastLogi(UserInfo userInfo)
        //{ 
        //    var 
        //}

        private bool AssignUserRole(int userId, int roleId)
        {
            try
            {
                if (userId > 0 && roleId > 0)
                {
                    _dbContext.UserRoles.Add(new UserRoles { UserId = userId, RoleId = roleId });
                    return _dbContext.SaveChanges() > 0;
                }

                return false;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
