using AutoMapper;
using System.Collections.Generic;

namespace Testing_Poc_Healthcare.Models
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PersonalDetails, PatientInfo>().ReverseMap();
            CreateMap<PatientAddress, AddressDetail>().ReverseMap();
            //CreateMap<List<PersonalDetails>, List<PatientInfo>>().ReverseMap();
            CreateMap<PatientInsurance, PatientInsuranceDetail>().ReverseMap();
        }
    }
}
