using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skyblock.API.AutoMapper;
using Skyblock.Common.Domain;
using Skyblock.Common.DTOs;
using Skyblock.Logic;

namespace Skyblock.API.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class AuctionsController : Controller
    {
        private readonly IAuctionFilterLogic logic;

        public AuctionsController(IAuctionFilterLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionDTO>>> GetFiltered([FromQuery] AuctionQuery query)
        {
            return Ok(await logic.FilterAuctionsAsync(query));
        }

        [HttpGet]
        [Route("bits")]
        public async Task<ActionResult<IEnumerable<AuctionDTO>>> GetBits()
        {
            return Ok(await logic.CalculateBitPricesAsync());
        }

        [HttpGet]
        [Route("accessories")]
        public async Task<ActionResult<IEnumerable<AuctionDTO>>> GetAccessories([FromQuery] AccessoryQuery query)
        {
            return Ok(await logic.CalculateAccessoryPricesAsync(query));
        }
    }
}