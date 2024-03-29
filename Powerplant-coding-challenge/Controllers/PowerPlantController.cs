using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Powerplant_coding_challenge.Model;
using Powerplant_coding_challenge.Service;
using System.Linq;

namespace Powerplant_coding_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerPlantController : Controller
    {
        private IPowerplantLogic PowerplanLogic { get; set; }

        public PowerPlantController(IPowerplantLogic ppLogic)
        {
            this.PowerplanLogic = ppLogic;
        }

        [HttpPost(Name = "productionplan")]
        public List<PowerplantsResult> ProductionPlan(PowerplantsInput productionPlan)
        {
            return PowerplanLogic.Compute(productionPlan);
        }


    }
}
