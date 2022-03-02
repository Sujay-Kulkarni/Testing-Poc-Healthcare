using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Testing_Poc_Healthcare.Interface;

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

    public class BenfitPlanList : ResponseStatus, IData<List<InsuranceInfo>>
    {
        public List<InsuranceInfo> Data { get ; set; }
    }

    public class AssignedBenefitPlan
    {
        public int PatientInsuranceId { get; set; }
        public string CompanyName { get; set; }
        public string PlanName { get; set; }
        public string InsuranceType { get; set; }
        public int Term { get; set; }
        public Status IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class AssignedBenfitPlanList : ResponseStatus, IData<List<AssignedBenefitPlan>>
    {
        public List<AssignedBenefitPlan> Data { get; set; }
    }
}
