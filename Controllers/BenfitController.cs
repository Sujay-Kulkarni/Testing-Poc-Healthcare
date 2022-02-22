using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.Interface;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            throw new NotImplementedException();
        }

        [HttpPost("AddBenfitPlan")]
        public ActionResult Add([FromBody] InsuranceInfo insuranceInfo)
        {
            var objResponse = _insuranceService.AddOrEditBenfitPlan(insuranceInfo);

            if(objResponse.Status == "Success")
            {
                return Ok(objResponse);
            } else
            {
                return BadRequest(objResponse);
            }
        }

        [HttpPut("EditBenfitPlan")]
        public ActionResult Edit([FromBody] InsuranceInfo insuranceInfo)
        {
            var objResponse = _insuranceService.AddOrEditBenfitPlan(insuranceInfo);

            if (objResponse.Status == "Success")
            {
                return Ok(objResponse);
            }
            else
            {
                return BadRequest(objResponse);
            }
        }

        [HttpDelete("DeleteBenfitPlan")]
        public ActionResult DeletePlan([FromQuery]int benfitPlanId)
        {
            throw new NotImplementedException();
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
