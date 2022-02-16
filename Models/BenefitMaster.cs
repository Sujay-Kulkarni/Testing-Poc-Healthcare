using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Testing_Poc_Healthcare.Models
{
    public class BenefitMaster
    {
        [Key]
        public int BenefitID { get; set; }

        [Required]        
        public string InsuranceType { get; set; }
        [Required]
        public string InsuranceName { get; set; }

        [Required]
        public string PlanProductName { get; set; }
        
    }

    public class BenefitDetail
    {
        public int BenefitID { get; set; }

        [Required]
        public string InsuranceType { get; set; }
        [Required]
        public string InsuranceName { get; set; }

        [Required]
        public string PlanProductName { get; set; }
    }


}
