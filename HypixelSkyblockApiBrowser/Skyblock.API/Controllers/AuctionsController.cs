using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Skyblock.API.AutoMapper;
using Skyblock.Domain;
using Skyblock.Logic;

namespace Skyblock.API.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class AuctionsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IAuctionFilterLogic logic;

        public AuctionsController(IMapper mapper, IAuctionFilterLogic logic)
        {
            this.mapper = mapper;
            this.logic = logic;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetFiltered([FromQuery] AuctionQuery query)
        {
            return Ok(await logic.FilterAuctionsAsync(query));
        }

        // GET
        [HttpGet]
        [Route("map")]
        public ActionResult<IClassA> GetMapped()
        {
            var dto = new ClassA_DTO { ID = 69 };
            var res = mapper.Map<IClassA>(dto);
            return Ok(res);
        }
    }
}