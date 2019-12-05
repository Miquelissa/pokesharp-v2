using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Pokesharp.Data;

namespace Pokesharp.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly Regions Regions;

        public RegionsController(Regions regions) {
            Regions = regions;
        }

        [HttpGet]
        [Route("")]
        public IActionResult List() {
            return Ok(Regions.List());
        }
    }
}