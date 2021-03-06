using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.DBContexts;
using Testing_Poc_Healthcare.Interface;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Services
{
    public class PatientService : IPatientService
    {
        private readonly HealthCareDBContext _dBContext;
        private readonly IMapper _mapper;
        private readonly IInsuranceService _insuranceService;
        public ILog logger;

        public PatientService(HealthCareDBContext healthCareDBContext, IMapper mapper, IInsuranceService service)
        {
            _dBContext = healthCareDBContext;
            _mapper = mapper;
            _insuranceService = service;
            logger = LogManager.GetLogger(typeof(PatientService));
        }
        public PatientResponse CreatePatient(PatientDetail patientDetail)
        {
            logger.Info("CreatePatient method called");
            logger.Info(JsonConvert.SerializeObject(patientDetail));

            try
            {
                var patientInfo = _mapper.Map<PatientInfo>(patientDetail.Personal);
                var patientAddress = _mapper.Map<PatientAddress>(patientDetail.Address);

                patientInfo.CreatedDate = DateTime.Now;
                patientInfo.CreatedBy = "Admin"; //login user name need to add
                patientInfo.ModifiedBy = "Admin"; // change to nullable data type

                _dBContext.PatientInfos.Add(patientInfo);
                var satus = _dBContext.SaveChanges() > 0;

                if(satus)
                {
                    logger.Info("Patient Created" + JsonConvert.SerializeObject(patientDetail));
                    CreatePatientAddress(patientInfo.PatientID, patientAddress);
                    logger.Info("Patient Created" + JsonConvert.SerializeObject(patientDetail));

                    return new PatientResponse { Status = "Success", Message = "Member created successfully", Data = patientInfo.PatientID };
                } else
                {
                    return new PatientResponse { Status = "Error", Message = "Error occured while enrolling member" };
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                return new PatientResponse { Status = "Error", Message = ex.Message };
            }
        }
        public PatientResponse CreatePatientAddress(int patientId, PatientAddress address)
        {
            logger.Info("CreatePatientAddress method called" + patientId);
            logger.Info(JsonConvert.SerializeObject(address));

            try
            {
                address.PatientId = patientId;

                _dBContext.PatientAddresses.Add(address);
                logger.Info("Created Patient Address");

                logger.Info(JsonConvert.SerializeObject(address));
                var status =  _dBContext.SaveChanges() > 0;
                if(status)
                {
                    return new PatientResponse { Status = "Success", Message = "Member address added successfully" };
                } else
                {
                    return new PatientResponse { Status = "Error", Message = "Member address added successfully" };
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new PatientResponse { Status = "Error", Message = ex.Message };
            }
        }
        public void DeletePatient()
        {
            throw new NotImplementedException();
        }
        public UpdatePatientResponse EditPatient(int memberId, PatientDetail patientDetail)
        {
            try
            {
                if((memberId != patientDetail.Personal.PatientID) || 
                    (memberId <= 0 && patientDetail.Personal.PatientID <=0))
                {
                    return new UpdatePatientResponse { Status = "Error", Message = "Invalid member Id" };
                } else
                {
                    var isValidMember = _dBContext.PatientInfos.Where(p => p.PatientID == memberId).FirstOrDefault();

                    if(isValidMember != null)
                    {
                        _dBContext.ChangeTracker.Clear();
                        var mappedObj = _mapper.Map<PatientInfo>(patientDetail.Personal);

                        mappedObj.ModifiedDate = DateTime.Now;
                        mappedObj.ModifiedBy = "Admin"; //Update logged in user name 
                        _dBContext.PatientInfos.Update(mappedObj);
                        var status = _dBContext.SaveChanges() > 0;
                        
                        if(status)
                        {
                            
                            var mappedAddress = _mapper.Map<PatientAddress>(patientDetail.Address);
                            mappedAddress.PatientId = mappedObj.PatientID;
                            _dBContext.PatientAddresses.Update(mappedAddress);
                            var isAddresUpdated = _dBContext.SaveChanges() > 0;
                        
                            if(isAddresUpdated) { return new UpdatePatientResponse { Status = "Success", Message = "Member details updated successfully", Data= patientDetail }; }
                            else { return new UpdatePatientResponse { Status = "Error", Message = "Error occured while updateing member address" }; }
                        }else { return new UpdatePatientResponse { Status = "Error", Message = "Error occured while updateing member details" }; }
                    } else { return new UpdatePatientResponse { Status = "Error", Message = "Member dose not exist" }; }
                }
            }catch(Exception ex)
            {
                return new UpdatePatientResponse { Status = "Error", Message = ex.Message };
            }
        }
        public List<PersonalDetails> FindPatient(PatientSearch patientSearch)
        {
            logger.Info("FindPatient method called");
            logger.Info(JsonConvert.SerializeObject(patientSearch));

            try
            {
                List<PatientInfo> patientInfos = new List<PatientInfo>();

                if (patientSearch.PatientId > 0)
                {
                    patientInfos = _dBContext.PatientInfos.Where(p => p.PatientID == patientSearch.PatientId).ToList();
                    logger.Info(JsonConvert.SerializeObject(patientInfos));
                }
                else if (!string.IsNullOrEmpty(patientSearch.FirstName))
                {
                    logger.Info(_mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.FirstName.Contains(patientSearch.FirstName)).ToList()));
                    patientInfos = _dBContext.PatientInfos.Where(p => p.FirstName.Contains(patientSearch.FirstName)).ToList();
                }
                else if (!string.IsNullOrEmpty(patientSearch.MiddleName))
                {
                    logger.Info(_mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.MiddleName.Contains(patientSearch.MiddleName)).ToList()));
                    patientInfos = _dBContext.PatientInfos.Where(p => p.MiddleName.Contains(patientSearch.MiddleName)).ToList();
                }
                else if (!string.IsNullOrEmpty(patientSearch.Lastname))
                {
                    logger.Info(_mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.LastName.Contains(patientSearch.Lastname)).ToList()));
                    patientInfos =  _dBContext.PatientInfos.Where(p => p.LastName.Contains(patientSearch.Lastname)).ToList();
                }
                else if (!string.IsNullOrEmpty(patientSearch.ContactNo))
                {
                    logger.Info(_mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.ContactNo == patientSearch.ContactNo).ToList()));
                    patientInfos =  _dBContext.PatientInfos.Where(p => p.ContactNo == patientSearch.ContactNo).ToList();
                } else if(patientSearch.DateOfBirth.HasValue)
                {
                    patientInfos = _dBContext.PatientInfos.Where(p => p.DateOfBirth.Date == patientSearch.DateOfBirth.Value).ToList();
                }

                if(patientInfos.Count() > 0)
                {
                    patientInfos = patientInfos.Skip(0).Take(50).ToList();
                    return _mapper.Map<List<PersonalDetails>>(patientInfos);
                }
                
                logger.Info("No records records");
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }

        }
        public MemberSummary PatientSummary(int patientId)
        {
            logger.Info("PatientSummary method called" + patientId);
            logger.Info(JsonConvert.SerializeObject(patientId));

            try
            {

                if (patientId > 0)
                {
                    var patientInfos = _dBContext.PatientInfos.Where(p => p.PatientID == patientId).FirstOrDefault();
                    var patientAddressInfo = _dBContext.PatientAddresses.Where(p => p.PatientId == patientId).FirstOrDefault();
                    var benefitDetails = _insuranceService.GetAllBenfitPlanByPatientId(patientId);
                    
                    return new MemberSummary
                    {
                        Personal = _mapper.Map<PersonalDetails>(patientInfos),
                        Address = _mapper.Map<AddressDetail>(patientAddressInfo),
                        BenfitPlanList = benefitDetails
                    };
                }
                else
                {
                    return new MemberSummary {  };
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }
    }
}
