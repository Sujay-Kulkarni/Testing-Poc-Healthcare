using System;
using System.ComponentModel;
using Testing_Poc_Healthcare.Interface;

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
        //[DefaultValue(10)]
        //public int PageSize { get; set; }
        //[DefaultValue(1)]
        //public int PageNumber { get; set; }
    }
}
