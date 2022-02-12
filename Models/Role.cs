using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Testing_Poc_Healthcare.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public ICollection<UserInfo> UserInfos { get; set; }
    }
}
