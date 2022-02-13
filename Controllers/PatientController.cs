using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.DBContexts;
using Testing_Poc_Healthcare.Interface;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PatientController : ControllerBase
    {
        //private readonly HealthCareDBContext _DBContext;
        //private readonly IMapper _mapper;
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            //_DBContext = healthCareDBContext;
            //_mapper = mapper;
            _patientService = patientService;
        }

        [HttpPost("Enrole")]
        public ActionResult Enrole([FromBody] PatientDetail patientDetail)
        {
            var response = _patientService.CreatePatient(patientDetail);
            if (response)
            {
                return Ok("Patient enrolled successfully");
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost("PatientSearch")]
        public ActionResult PatientSearch([FromBody] PatientSearch patientSearch)
        {
            var response = _patientService.FindPatient(patientSearch);

            if(response != null)
            {
                return Ok(response);
            }

            return BadRequest("Patient not found");
        }

    }
}
