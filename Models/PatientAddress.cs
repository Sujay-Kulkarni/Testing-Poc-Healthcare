using System.ComponentModel.DataAnnotations;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientAddress
    {
        [Key]
        public int PatientAddressID { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public PatientInfo PatientID { get; set; }
    }
    public class AddressDetail
    {
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}
