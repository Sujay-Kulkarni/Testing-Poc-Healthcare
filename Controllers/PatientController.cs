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
using log4net;
using Newtonsoft.Json;

namespace Testing_Poc_Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PatientController : ControllerBase
    {
        //private readonly HealthCareDBContext _DBContext;
        //private readonly IMapper _mapper;
        private readonly IPatientService _patientService;
        private readonly ILog logger;
        public PatientController(IPatientService patientService)
        {
            //_DBContext = healthCareDBContext;
            //_mapper = mapper;
            _patientService = patientService;
            logger = LogManager.GetLogger(typeof(PatientController));
        }

        [HttpPost("Enrole")]
        public ActionResult Enrole([FromBody] PatientDetail patientDetail)
        {
            logger.Info("Enrole method called");
            logger.Info(JsonConvert.SerializeObject(patientDetail));
            try
            {
                var response = _patientService.CreatePatient(patientDetail);
                if (response)
                {
                    logger.Info("Patient enrolled successfully " +JsonConvert.SerializeObject(response));
                    return Ok("Patient enrolled successfully");
                }
                logger.Info("Something went wrong");
                return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("PatientSearch")]
        public ActionResult PatientSearch([FromBody] PatientSearch patientSearch)
        {
            logger.Info("PatientSearch method called");
            logger.Info(JsonConvert.SerializeObject(patientSearch));
            try
            {
                var response = _patientService.FindPatient(patientSearch);

                if (response != null)
                {
                    logger.Info(JsonConvert.SerializeObject(response));
                    return Ok(response);
                }

                logger.Info("Patient not found");
                return BadRequest("Patient not found");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest("Something went wrong");
            }
        }


        [HttpPost("AddUser")]
        public ActionResult AddBenefit(Benefit benefits)
        {
            logger.Info("AddBenefit method called");
            logger.Info(JsonConvert.SerializeObject(benefits));

            try
            {
                var response = _patientService.AddBenefit(benefits);

                if (response)
                {
                    logger.Info("User created successfully " + JsonConvert.SerializeObject(response));
                    return Ok("User created successfully");
                }

                logger.Info("User already exists");
                return NotFound("User already exists");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest("Somethinng Went Wrong!");
            }
        }

    }
}
