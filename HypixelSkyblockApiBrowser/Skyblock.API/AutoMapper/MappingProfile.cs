using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyblock.API.AutoMapper
{
    public class ClassA_DTO
    {
        public int ID { get; set; }
    }
    public interface IClassA
    {
        public int ID { get; set; }
    }
    public class ClassA : IClassA
    {
        public int ID { get; set; }
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClassA_DTO, IClassA>().As<ClassA>();
            CreateMap<ClassA_DTO, ClassA>();
        }
    }
}
