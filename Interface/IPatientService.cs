using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Interface
{
    public interface IPatientService
    {
        PatientResponse CreatePatient(PatientDetail patientDetail);
        PatientResponse CreatePatientAddress(int patientId, PatientAddress address);
        void EditPatient();
        void DeletePatient();
        List<PersonalDetails> FindPatient(PatientSearch patientSearch);
        //bool AddBenefit(BenefitMaster benefit);
        MemberSummary PatientSummary(int patientID);
    }
}
