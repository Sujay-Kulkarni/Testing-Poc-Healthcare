using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientInfo
    {
        [Key]
        public int PatientID { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [DefaultValue(true)]
        public bool Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Age { get; set; }
        [EmailAddress(ErrorMessage ="Please enter valid email id")]
        public string Email { get; set; }
        [Required]
        public string ContactNo { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class PersonalDetails
    {
        [DefaultValue(0)]
        public int PatientID { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [DefaultValue(true)]
        public bool Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Age { get; set; }
        [EmailAddress(ErrorMessage = "Please enter valid email id")]
        public string Email { get; set; }
        [Required]
        public string ContactNo { get; set; }
    }
}
