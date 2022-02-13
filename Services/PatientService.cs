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
    public class PatientService : IPatientService
    {
        private readonly HealthCareDBContext _dBContext;
        private readonly IMapper _mapper;

        public PatientService(HealthCareDBContext healthCareDBContext, IMapper mapper)
        {
            _dBContext = healthCareDBContext;
            _mapper = mapper;
        }
        public bool CreatePatient(PatientDetail patientDetail)
        {
            var patientInfo = _mapper.Map<PatientInfo>(patientDetail.Personal);
            var patientAddress = _mapper.Map<PatientAddress>(patientDetail.Address);

            patientInfo.CreatedDate = DateTime.Now;
            patientInfo.CreatedBy = "Admin"; //login user name need to add
            patientInfo.ModifiedBy = "Admin"; // change to nullable data type
            
            _dBContext.PatientInfos.Add(patientInfo);
            _dBContext.SaveChanges();
            
            return CreatePatientAddress(patientInfo.PatientID, patientAddress);
        }

        public bool CreatePatientAddress(int patientId, PatientAddress address)
        {
            address.PatientId = patientId;
            
            _dBContext.PatientAddresses.Add(address);
            return _dBContext.SaveChanges() > 0;
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
            if (patientSearch.PatientId > 0)
            {
                var patientInfos = _dBContext.PatientInfos.Where(p => p.PatientID == patientSearch.PatientId).ToList();
                return _mapper.Map<List<PersonalDetails>>(patientInfos);
            }
            else if (patientSearch.FirstName != null || patientSearch.FirstName != "")
            {
                return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.FirstName.Contains(patientSearch.FirstName)).ToList());
            }
            else if (patientSearch.MiddleName != null || patientSearch.MiddleName != "")
            {
                return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.MiddleName.Contains(patientSearch.MiddleName)).ToList());
            }
            else if (patientSearch.Lastname != null || patientSearch.Lastname != "")
            {
                return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.Lastname.Contains(patientSearch.Lastname)).ToList());
            }
            else if (patientSearch.ContactNo != null || patientSearch.ContactNo != "")
            {
                return _mapper.Map<List<PersonalDetails>>(_dBContext.PatientInfos.Where(p => p.ContactNo == patientSearch.ContactNo).ToList());
            }

            return null;
        }
    }
}
