using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Testing_Poc_Healthcare.Models
{
    public class Benefit
    {
        [Key]
        public int BenefitID { get; set; }
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
        public string InsuranceType { get; set; }
        [Required]
        public string InsuranceName { get; set; }

        [Required]
        public string PlanProductName { get; set; }

        [Required]
        public string SubscriberId { get; set; }
        public string CreatedBy { get; set; }        
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    
}
