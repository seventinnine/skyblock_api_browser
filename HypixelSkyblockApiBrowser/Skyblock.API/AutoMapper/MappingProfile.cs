using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skyblock.Common.Domain;
using Skyblock.Common.DTOs;
using Skyblock.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyblock.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionDTO>();
            CreateMap<BitPrice, BitPriceDTO>();
            CreateMap<AccessoryPrice, AccessoryPriceDTO>();
        }
    }
}
