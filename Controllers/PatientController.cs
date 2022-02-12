using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.DBContexts;
using Testing_Poc_Healthcare.Models;

namespace Testing_Poc_Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly HealthCareDBContext _DBContext;
        public PatientController(HealthCareDBContext healthCareDBContext)
        {
            _DBContext = healthCareDBContext;
        }

        [HttpPost]
        public ActionResult Enrole([FromBody] PatientDetail patientDetail)
        {
            return BadRequest();
        }

    }
}
