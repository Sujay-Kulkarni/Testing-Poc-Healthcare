using System.Collections.Generic;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Interface
{
    public interface IInsuranceService
    {
        ResponseStatus AddBenfitPlan(InsuranceInfo insuranceInfo);
        ResponseStatus EditBenfitPlan(InsuranceInfo insuranceInfo);
        ResponseStatus AssiganBenfitPlan(PatientInsuranceDetail insuranceDetail);
        ResponseStatus ChangeBenfitPlan(PatientInsuranceDetail insuranceDetail);
        BenfitPlanList GetAllPlans();
        BenfitPlanList GetAllBenfitPlanByPatientId(int patitentId);
        ResponseStatus RemoveBenfitPlan(int insuranceInfoId);
    }
}
