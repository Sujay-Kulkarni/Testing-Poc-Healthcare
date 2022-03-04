using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.Interface;

namespace Testing_Poc_Healthcare.Models
{
    public class UpdatePatientResponse : ResponseStatus, IData<PatientDetail>
    {
        public PatientDetail Data { get; set; }
    }
}
