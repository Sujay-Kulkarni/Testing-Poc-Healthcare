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
        public ILog logger;

        public PatientService(HealthCareDBContext healthCareDBContext, IMapper mapper)
        {
            _dBContext = healthCareDBContext;
            _mapper = mapper;
            logger = LogManager.GetLogger(typeof(PatientService));
        }
        public bool CreatePatient(PatientDetail patientDetail)
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
                _dBContext.SaveChanges();

                logger.Info("Patient Created" + JsonConvert.SerializeObject(patientDetail));
                return CreatePatientAddress(patientInfo.PatientID, patientAddress);
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }

        public bool CreatePatientAddress(int patientId, PatientAddress address)
        {
            logger.Info("CreatePatientAddress method called" + patientId);
            logger.Info(JsonConvert.SerializeObject(address));

            try
            {
                address.PatientId = patientId;

                _dBContext.PatientAddresses.Add(address);
                logger.Info("Created Patient Address");

                logger.Info(JsonConvert.SerializeObject(address));
                return _dBContext.SaveChanges() > 0;                
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }

        public void DeletePatient()
        {
            throw new NotImplementedException();
        }
        public void EditPatient()
        {
            throw new NotImplementedException();
        }
        public List<PersonalDetails> FindPatient(PatientSearch patientSearch)
        {

            logger.Info("FindPatient method called");
            logger.Info(JsonConvert.SerializeObject(patientSearch));

            try
            {
                if (patientSearch.PatientId > 0)
                {
                    var patientInfos = _dBContext.PatientInfos.Where(p => p.PatientID == patientSearch.PatientId).ToList();
                    logger.Info(JsonConvert.SerializeObject(patientInfos));
                    return _mapper.Map<List<PersonalDetails>>(patientInfos);
                }
                else if (!string.IsNullOrEmpty(patientSearch.FirstName))
                {
                    logger.Info(_mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.FirstName.Contains(patientSearch.FirstName)).ToList()));
                    return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.FirstName.Contains(patientSearch.FirstName)).ToList());
                }
                else if (!string.IsNullOrEmpty(patientSearch.MiddleName))
                {
                    logger.Info(_mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.MiddleName.Contains(patientSearch.MiddleName)).ToList()));
                    return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.MiddleName.Contains(patientSearch.MiddleName)).ToList());
                }
                else if (!string.IsNullOrEmpty(patientSearch.Lastname))
                {
                    logger.Info(_mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.LastName.Contains(patientSearch.Lastname)).ToList()));
                    return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.LastName.Contains(patientSearch.Lastname)).ToList());
                }
                else if (!string.IsNullOrEmpty(patientSearch.ContactNo))
                {
                    logger.Info(_mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.ContactNo == patientSearch.ContactNo).ToList()));
                    return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.ContactNo == patientSearch.ContactNo).ToList());
                } else if(patientSearch.DateOfBirth.HasValue)
                {
                    return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.DateOfBirth.Date == patientSearch.DateOfBirth.Value));
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
        public bool AddBenefit(BenefitMaster benefit)
        {
            logger.Info("AddBenefit method called" + benefit);
            logger.Info(JsonConvert.SerializeObject(benefit));

            try
            {
                _dBContext.Benefitmaster.Add(benefit);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }
        public PatientDetail PatientSummary(int patientId)
        {
            logger.Info("PatientSummary method called" + patientId);
            logger.Info(JsonConvert.SerializeObject(patientId));

            try
            {

                if (patientId > 0)
                {
                    var patientInfos = _dBContext.PatientInfos.Where(p => p.PatientID == patientId).FirstOrDefault();
                    var patientAddressInfo = _dBContext.PatientAddresses.Where(p => p.PatientId == patientId).FirstOrDefault();
                    //var patientdetails = _mapper.Map<PatientDetail>(patientInfos);
                    return new PatientDetail
                    {
                        Personal = _mapper.Map<PersonalDetails>(patientInfos),
                        Address = _mapper.Map<AddressDetail>(patientAddressInfo)
                    };
                }
                else
                {
                    return null;
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
