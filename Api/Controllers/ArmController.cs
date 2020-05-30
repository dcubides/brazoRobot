using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Interfaces.Business;
using CommonLibrary.Entities.Angle;
using CommonLibrary.Entities.Arm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmController : ControllerBase
    {
        private IOperationArm Module = null;
        public ArmController(IOperationArm module)
        {
            this.Module = module;
        }


        [HttpGet]
        public Task<Arm> get() {
            Arm objReturn = Module.ManipulateArm();
            return Task.FromResult(objReturn);
        }

        [HttpPost]
        public Task<Arm> post(Controls controls)
        {           
            Arm objReturn = Module.AlterArm(controls);
            return Task.FromResult(objReturn);
        }
    }
}
