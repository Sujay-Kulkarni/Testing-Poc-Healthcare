using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientDetail
    {
        public PersonalDetails Personal { get; set; }
        public AddressDetail Address { get; set; }
    }
}
