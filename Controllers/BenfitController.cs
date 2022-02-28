using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Testing_Poc_Healthcare.Interface;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BenfitController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;
        public BenfitController(IInsuranceService service)
        {
            _insuranceService = service;
        }
        [HttpGet("GetAllBenfitPlan")]
        public ActionResult GetAllPlan()
        {
            var objReponse = _insuranceService.GetAllPlans();
            if (objReponse.Status == "Success")
            {
                return Ok(objReponse);
            }
            return BadRequest(objReponse);
        }

        [HttpPost("AddBenfitPlan")]
        public ActionResult Add([FromBody] InsuranceInfo insuranceInfo)
        {
            var objResponse = _insuranceService.AddBenfitPlan(insuranceInfo);

            if (objResponse.Status == "Success")
            {
                return Ok(objResponse);
            } else
            {
                return BadRequest(objResponse);
            }
        }

        [HttpPut("{benfitPlanId}")]
        public ActionResult Edit(int benfitPlanId, InsuranceInfo insuranceInfo)
        {
            if(benfitPlanId != insuranceInfo.InsuranceInfoId)
            {
                return BadRequest(new ResponseStatus { Status = "Error", Message = "Invalid benfit id" });
            }
            var objResponse = _insuranceService.EditBenfitPlan(insuranceInfo);

            if (objResponse.Status == "Success")
            {
                return Ok(objResponse);
            }
            else
            {
                return BadRequest(objResponse);
            }
        }
        
        [HttpDelete("{benfitPlanId}")]
        public ActionResult DeletePlan(int benfitPlanId)
        {
            var response = _insuranceService.RemoveBenfitPlan(benfitPlanId);
            if(response.Status == "Success")
            {
                return Ok(response);
            } else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("AssignBenfitPlan")]
        public ActionResult AssignBenfitPlan([FromBody] PatientInsuranceDetail insuranceDetail)
        {
            var response = _insuranceService.AssiganBenfitPlan(insuranceDetail);
            if (response.Status == "Scccess")
            {
                return Ok(response);
            }
            else
                return BadRequest(response);
        }
    }
}
