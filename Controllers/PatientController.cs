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
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILog logger;
        public PatientController(IPatientService patientService)
        {
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

                if (response.Count() > 0)
                {
                    logger.Info(JsonConvert.SerializeObject(response));
                    return Ok(response);
                }

                logger.Info("Member not found");
                return BadRequest("Member not found");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("GetPatientSummary")]
        public ActionResult GetPatientSummary([FromQuery]int patientID)
        {
            logger.Info("PatientSummary method called");
            logger.Info(JsonConvert.SerializeObject(patientID));
            try
            {
                var response = _patientService.PatientSummary(patientID);

                if (response != null)
                {
                    logger.Info(JsonConvert.SerializeObject(response));
                    return Ok(response);
                }

                logger.Info("Patient information not found");
                return BadRequest("Patient information not found");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return BadRequest("Something went wrong");
            }

        }

    }
}
