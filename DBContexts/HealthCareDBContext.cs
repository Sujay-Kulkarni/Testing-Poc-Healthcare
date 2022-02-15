using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.DBContexts
{
    public class HealthCareDBContext : DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<PatientInfo> PatientInfos { get; set; }
        public DbSet<PatientAddress> PatientAddresses { get; set; }
        public DbSet<Benefit> Benefits { get; set; }

        public HealthCareDBContext(DbContextOptions<HealthCareDBContext> options) : base(options)
        {
        }


    }
}
