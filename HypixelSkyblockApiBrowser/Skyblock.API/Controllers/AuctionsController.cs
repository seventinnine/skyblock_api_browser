using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skyblock.Domain;
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

        // GET
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetFiltered([FromQuery] AuctionQuery query)
        {
            return Ok(await logic.FilterAuctionsAsync(query));
        }
    }
}