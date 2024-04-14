using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
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
            var regionsDomain = dbContext.Regions.ToList();

            var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id= regionDomain.Id,
                    Name= regionDomain.Name,
                    Code= regionDomain.Code,
                    RegionImageUrl= regionDomain.RegionImageUrl,
                }
                );
            }
            return Ok(regionDto);

        }

        [HttpGet]
        [Route("{id:Guid}")]

        public IActionResult GetById([FromRoute] Guid id)
        {

            var regionDomain = dbContext.Regions.Find(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
              return Ok(regionDto);
            
        }




        [HttpPost]

        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            var regionDomainModel = new Region
            {

                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl


            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();


            var regionDto = new RegionDto
            {

                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id=regionDomainModel.Id}, regionDto);
           
        }
    }
}
