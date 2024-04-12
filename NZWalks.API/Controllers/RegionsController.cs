using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NZWalks.API.Data;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public IActionResult GetAllRegions()
        {
            var regions = dbContext.Regions.ToList();
            return Ok(regions);

        }

        [HttpGet]
        [Route("{id:Guid}")]

        public IActionResult GetById([FromRoute] Guid id)
        {

            var regions = dbContext.Regions.Find(id);

            if (regions == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(regions);
            }



        }
    }
}
