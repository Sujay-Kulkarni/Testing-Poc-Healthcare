using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Testing_Poc_Healthcare.Models
{
    public class InsuranceInfo
    {
        [Key]
        public int InsuranceInfoId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string PlanName { get; set; }
        [Required]
        public string InsuranceType { get; set; }
        [Required]
        public int PlanDuration { get; set; }
        [Required]
        public DateTime PlanStartDate { get; set; }
        [Required]
        public DateTime PlanEndDate { get; set; }
        [DefaultValue(true)]
        [Required]
        public bool IsAcive { get; set; }
        public string Description { get; set; }
    }
}
