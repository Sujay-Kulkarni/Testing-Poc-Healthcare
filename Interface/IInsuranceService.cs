using System.Collections.Generic;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Interface
{
    public interface IInsuranceService
    {
        BenefitResponse AddBenfitPlan(InsuranceInfo insuranceInfo);
        BenefitResponse EditBenfitPlan(InsuranceInfo insuranceInfo);
        BenefitResponse AssiganBenfitPlan(PatientInsuranceDetail insuranceDetail);
        BenefitResponse ChangeBenfitPlan(PatientInsuranceDetail insuranceDetail);
        BenfitPlanList GetAllPlans();
        AssignedBenfitPlanList GetAllBenfitPlanByPatientId(int patitentId);
        ResponseStatus RemoveBenfitPlan(int insuranceInfoId);
        ResponseStatus TerminateAssignedBenefitPlan(int assignedPlanId);
    }
}
