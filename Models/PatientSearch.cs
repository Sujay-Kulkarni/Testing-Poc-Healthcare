using System;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientSearch
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }
        public string ContactNo { get; set; }
    }
}
