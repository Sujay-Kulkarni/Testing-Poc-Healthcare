using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientBenefit
    {
        [Key]
        public int PatientBenefitID { get; set; }

        [ForeignKey("BenefitID")]
        public virtual BenefitMaster BenefitMaster { get; set; }

        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
       
        [Required]
        public string SubscriberId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }


       


    }
}
