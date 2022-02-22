using System.Collections.Generic;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Interface
{
    public interface IInsuranceService
    {
        ResponseStatus AddOrEditBenfitPlan(InsuranceInfo insuranceInfo);
        ResponseStatus AssiganBenfitPlan(PatientInsuranceDetail insuranceDetail);
        ResponseStatus ChangeBenfitPlan(PatientInsuranceDetail insuranceDetail);
        List<string> GetAllPlans();
    }
}
