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
        public ResponseStatus AddOrEditBenfitPlan(InsuranceInfo insuranceInfo)
        {
            try {
                var benfitPlanDetails = _dBContext.InsuranceInfos
                                        .Where(i => i.InsuranceInfoId == insuranceInfo.InsuranceInfoId)
                                        .FirstOrDefault();
                if (benfitPlanDetails == null)
                { 
                    var isPlanExists = _dBContext.InsuranceInfos
                                        .Where(i => i.CompanyName == insuranceInfo.CompanyName && i.PlanName == insuranceInfo.PlanName)
                                        .Count() > 0;
                    if (!isPlanExists)
                    {
                        _dBContext.InsuranceInfos.Add(insuranceInfo);
                        var result = _dBContext.SaveChanges() > 0;
                        if (result)
                            return new ResponseStatus
                            {
                                Status = "Success",
                                Message = "Benfit plan added successfully"
                            };
                        else
                            return new ResponseStatus { 
                            Status="Error",
                            Message="Error occured while saving the recored"
                            };
                    }
                    return new ResponseStatus
                    {
                        Status = "Error",
                        Message = "Record already exists"
                    };
                } else
                {
                    _dBContext.ChangeTracker.Clear(); //Clear the entity tracking
                    _dBContext.InsuranceInfos.Update(insuranceInfo);
                   var result = _dBContext.SaveChanges() > 0;
                    if (result)
                        return new ResponseStatus
                        {
                            Status = "Success",
                            Message = "Record updated successfully"
                        };
                    else
                        return new ResponseStatus
                        {
                            Status = "Error",
                            Message = "Error occured while updating the recored"
                        };
                }
            
            } catch (Exception ex)
            {
                return new ResponseStatus
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        public ResponseStatus AssiganBenfitPlan(PatientInsuranceDetail insuranceDetail)
        {
            try
            {
                if (insuranceDetail.PatientId <= 0 || insuranceDetail.InsuranceId <= 0)
                {
                    return new ResponseStatus
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
                            return new ResponseStatus { Status = "Success", Message = "Benfit plan assigned to the member" };
                        }
                        else
                        {
                            return new ResponseStatus { Status = "Error", Message = "Error occured while assiging benfit plan" };
                        }
                    }
                    else
                    {
                        return new ResponseStatus { Status = "Error", Message = "Benfit plan assigned to the mmber" };
                    }
                    
                }
            } catch(Exception ex)
            {
                return new ResponseStatus
                {
                    Status = "Error",
                    Message = ex.Message
                };
            }
        }

        public List<string> GetAllPlans()
        {
            throw new NotImplementedException();
        }

        public ResponseStatus ChangeBenfitPlan(PatientInsuranceDetail insuranceDetail)
        {
            using var transaction = _dBContext.Database.BeginTransaction();
            try {
                if (insuranceDetail.PatientId <= 0 || insuranceDetail.InsuranceId <= 0) {
                    return new ResponseStatus { Status = "Error", Message = "Member/Benfit id is incorrect" };
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

                                if (isNewPlanAssigned) { return new ResponseStatus { Status = "Sucess", Message = "Benfit plan changed successfully " }; }
                                else { 
                                    transaction.RollbackToSavepoint("BeforeBenfitPlanRemove");
                                    return new ResponseStatus { Status = "Error", Message = "Error occured while changing benfit plan" };
                                }
                            } else { return new ResponseStatus { Status = "Error", Message = "Error occured while removing existing plan" }; }
                        } else { return new ResponseStatus { Status = "Error", Message = "No active benfit plan assigned to member" }; }

                    }
                    return new ResponseStatus { Status = "Error", Message = "Existing benfit plan details are missing" };
                }
            } catch(Exception ex) {
                transaction.RollbackToSavepoint("BeforeBenfitPlanRemove");
                return new ResponseStatus { Status = "Error", Message = ex.Message };
            }
        }
    }
}
