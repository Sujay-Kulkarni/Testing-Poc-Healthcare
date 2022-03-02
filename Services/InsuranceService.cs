using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.DBContexts;
using Testing_Poc_Healthcare.Interface;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly HealthCareDBContext _dBContext;
        private readonly IMapper _mapper;
        public InsuranceService(HealthCareDBContext healthCareDB,IMapper mapper)
        {
            _dBContext = healthCareDB;
            _mapper = mapper;
        }
        public BenefitResponse AddBenfitPlan(InsuranceInfo insuranceInfo)
        {
            try {
                    var isPlanExists = _dBContext.InsuranceInfos
                                        .Where(i => i.CompanyName == insuranceInfo.CompanyName && i.PlanName == insuranceInfo.PlanName)
                                        .Count() > 0;
                    if (!isPlanExists)
                    {
                        insuranceInfo.PlanName = string.Concat(insuranceInfo.CompanyName, "-", insuranceInfo.PlanName).ToUpper();
                        _dBContext.InsuranceInfos.Add(insuranceInfo);
                        var result = _dBContext.SaveChanges() > 0;
                        if (result)
                            return new BenefitResponse {
                                Status = "Success",
                                Message = "Benfit plan added successfully",
                                Data = insuranceInfo.InsuranceInfoId
                            };
                        else
                            return new BenefitResponse { 
                            Status="Error",
                            Message="Error occured while saving the benfit plan"
                            };
                    }
                    return new BenefitResponse
                    {
                        Status = "Error",
                        Message = "Benfit plan already exists"
                    };
            } catch (Exception ex)
            {
                return new BenefitResponse
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        public BenefitResponse AssiganBenfitPlan(PatientInsuranceDetail insuranceDetail)
        {
            try
            {
                if (insuranceDetail.PatientId <= 0 || insuranceDetail.InsuranceId <= 0)
                {
                    return new BenefitResponse
                    {
                        Status = "Error",
                        Message = "Member/Benfit id is incorrect"
                    };
                }
                else
                {
                    var getAssignedPlan = _dBContext.PatientInsurances
                                            .Where(i => i.PatientId == insuranceDetail.PatientId &&
                                                    i.InsuranceId == insuranceDetail.InsuranceId).FirstOrDefault();
                    if(getAssignedPlan == null)
                    {
                        var mapModel = _mapper.Map<PatientInsurance>(insuranceDetail);
                        _dBContext.PatientInsurances.Add(mapModel);
                        var result = _dBContext.SaveChanges() > 0;
                        if (result)
                        {
                            return new BenefitResponse { Status = "Success", Message = "Benfit plan assigned to the member", Data=mapModel.InsuranceId };
                        }
                        else
                        {
                            return new BenefitResponse { Status = "Error", Message = "Error occured while assiging benfit plan" };
                        }
                    }
                    else
                    {
                        return new BenefitResponse { Status = "Error", Message = "Benfit plan assigned to the mmber" };
                    }
                    
                }
            } catch(Exception ex)
            {
                return new BenefitResponse
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        public BenfitPlanList GetAllPlans()
        {
            try
            {
                var lstBenfitPlan = _dBContext.InsuranceInfos.OrderBy(o => o.InsuranceInfoId).ToList();
                if (lstBenfitPlan.Count() > 0)
                {
                    return new BenfitPlanList { Status = "Success", Message = string.Empty, Data = lstBenfitPlan };
                }
                return new BenfitPlanList { Status = "Error", Message = "No record found", Data = null };
            }
            catch (Exception ex)
            {
                return new BenfitPlanList
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public BenefitResponse ChangeBenfitPlan(PatientInsuranceDetail insuranceDetail)
        {
            using var transaction = _dBContext.Database.BeginTransaction();
            try {
                if (insuranceDetail.PatientId <= 0 || insuranceDetail.InsuranceId <= 0) {
                    return new BenefitResponse { Status = "Error", Message = "Member/Benfit id is incorrect" };
                } else { 
                    if(insuranceDetail.OldBenfitPlanId.HasValue)
                    {
                        var existingPlan = _dBContext.PatientInsurances
                                                        .Where(e => e.InsuranceId == insuranceDetail.OldBenfitPlanId.Value && e.IsActive == Status.Active)
                                                        .FirstOrDefault();
                        if(existingPlan != null)
                        {
                            transaction.CreateSavepoint("BeforeBenfitPlanRemove");
                            existingPlan.IsActive = Status.Terminited;
                            _dBContext.PatientInsurances.Update(existingPlan);
                            var isTerminited = _dBContext.SaveChanges() > 0;

                            if(isTerminited)
                            {
                                var mappedModel = _mapper.Map<PatientInsurance>(insuranceDetail);
                                _dBContext.PatientInsurances.Add(mappedModel);
                                var isNewPlanAssigned = _dBContext.SaveChanges() > 0;

                                if (isNewPlanAssigned) { return new BenefitResponse { Status = "Sucess", Message = "Benfit plan changed successfully " }; }
                                else { 
                                    transaction.RollbackToSavepoint("BeforeBenfitPlanRemove");
                                    return new BenefitResponse { Status = "Error", Message = "Error occured while changing benfit plan" };
                                }
                            } else { return new BenefitResponse { Status = "Error", Message = "Error occured while removing existing plan" }; }
                        } else { return new BenefitResponse { Status = "Error", Message = "No active benfit plan assigned to member" }; }

                    }
                    return new BenefitResponse { Status = "Error", Message = "Existing benfit plan details are missing" };
                }
            } catch(Exception ex) {
                transaction.RollbackToSavepoint("BeforeBenfitPlanRemove");
                return new BenefitResponse { Status = "Error", Message = ex.Message };
            }
        }

        public AssignedBenfitPlanList GetAllBenfitPlanByPatientId(int patitentId)
        {
            try {
                if(patitentId <= 0 )
                {
                    return new AssignedBenfitPlanList { Status = "Error", Message = "Invalid patientId", Data = null };
                } else
                {
                    var isValidPatientId = _dBContext.PatientInfos.Where(p => p.PatientID == patitentId).Count() == 1;
                    if(isValidPatientId)
                    {
                        List<AssignedBenefitPlan> assignedPlans = (from p in _dBContext.PatientInsurances
                                             join i in _dBContext.InsuranceInfos
                                             on p.InsuranceId equals i.InsuranceInfoId
                                             where p.PatientId == patitentId
                                             select new AssignedBenefitPlan { 
                                                 PatientInsuranceId = p.PatientInsuranceId,
                                                 CompanyName = i.CompanyName,
                                                 PlanName = i.PlanName,
                                                 InsuranceType =  i.InsuranceType,
                                                 Term =  i.PlanDuration,
                                                 StartDate = p.StartDate,
                                                 EndDate = p.EndDate,
                                                 IsActive = p.IsActive
                                             }
                                             ).ToList();
                       
                        if(assignedPlans.Count() > 0)
                        {
                            return new AssignedBenfitPlanList { Status = "Success", Message = "", Data = assignedPlans };
                        }
                        else
                        {
                            return new AssignedBenfitPlanList { Status = "Success", Message = "No record found", Data = null };
                        }
                    } else
                    {
                        return new AssignedBenfitPlanList { Status = "Error", Message = "Invalid patientId", Data = null };
                    }
                }
            } catch(Exception ex)
            {
                return new AssignedBenfitPlanList { Status = "Error", Message = ex.Message, Data = null };
            }
        }

        public ResponseStatus RemoveBenfitPlan(int insuranceInfoId)
        {
            try
            {
                if(insuranceInfoId >0)
                {
                    var getBenfitPlan = _dBContext.InsuranceInfos.Where(e => e.InsuranceInfoId == insuranceInfoId).FirstOrDefault();

                    if(getBenfitPlan != null)
                    {
                        _dBContext.InsuranceInfos.Remove(getBenfitPlan);
                        var status = _dBContext.SaveChanges() > 0;
                        if(status)
                        {
                            return new ResponseStatus { Status = "Success", Message = "Benfit plan removed successfully" };
                        } else
                            return new ResponseStatus { Status = "Error", Message = "Error occured while removing benfit plan" };
                    } else
                        return new ResponseStatus { Status = "Error", Message = "Benfit plan id dosenot exist" };
                }
                else
                    return new ResponseStatus { Status = "Error", Message = "Invalid benfit plan id" };
            }
            catch (Exception ex)
            {
                return new ResponseStatus { Status = "Error", Message = ex.Message };
            }
        }

        public BenefitResponse EditBenfitPlan(InsuranceInfo insuranceInfo)
        {
            try
            {
                var benfitPlanDetails = _dBContext.InsuranceInfos
                                        .Where(i => i.InsuranceInfoId == insuranceInfo.InsuranceInfoId)
                                        .FirstOrDefault();
                if (benfitPlanDetails == null)
                {
                    return new BenefitResponse { Status = "Error", Message = "benefits id doesn't exist" };
                }
                else
                {
                    _dBContext.ChangeTracker.Clear(); //Clear the entity tracking
                    _dBContext.InsuranceInfos.Update(insuranceInfo);
                    var result = _dBContext.SaveChanges() > 0;
                    if (result)
                        return new BenefitResponse
                        {
                            Status = "Success",
                            Message = "Benfit plan updated successfully"
                        };
                    else
                        return new BenefitResponse
                        {
                            Status = "Error",
                            Message = "Error occured while updating the benfit plan"
                        };
                }

            }
            catch (Exception ex)
            {
                return new BenefitResponse
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

    }
}
