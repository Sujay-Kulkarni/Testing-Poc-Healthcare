using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientInsurance
    {
        [Key]
        public int PatientInsuranceId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int InsuranceId { get; set; }
        [Required]
        public int Term { get; set; }
        [Required]
        [DefaultValue(Status.Active)]
        public Status IsActive { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey("PatientId")]
        public virtual PatientInfo PatientInfo { get; set; }

        [ForeignKey("InsuranceId")]
        public virtual InsuranceInfo InsuranceInfo { get; set; }
    }

    public class PatientInsuranceDetail
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int InsuranceId { get; set; }
        [Required]
        public int Term { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public Status IsActive { get; set; }
        public int? OldBenfitPlanId { get; set; }
    }

    public enum Status
    {
        Active,
        Terminited
    }
}
