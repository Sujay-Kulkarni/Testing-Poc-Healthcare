using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Testing_Poc_Healthcare.Models
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<Role> Roles { get; set; }
    }

    [NotMapped]
    public class UserInfoVM {
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string EmailId { get; set; }
        public string RoleName { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
