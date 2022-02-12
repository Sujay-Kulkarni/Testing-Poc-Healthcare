using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Interface
{
    public interface IPatientService
    {
        bool CreatePatient(PatientInfo patientInfo);
        void EditPatient();
        void DeletePatient();
        void FindPatient();
    }
}
