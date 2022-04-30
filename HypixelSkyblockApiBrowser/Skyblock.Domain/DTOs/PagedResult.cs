using AutoMapper;
using Newtonsoft.Json;
using Skyblock.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common.DTOs
{
    public class PagedResult<T>
    {
        [JsonProperty(Required =Required.Always)]
        public int Page { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int TotalPages { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int ItemsPerPage { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int TotalItems { get; set; }

        [JsonProperty(Required = Required.Always)]
        public IList<T> Data { get; set; }

        public PagedResult()
        {
            Page = 1;
            ItemsPerPage = 0;
            TotalPages = 1;
            TotalItems = 0;
            Data = new List<T>();
        }

        public PagedResult(int page, int totalPages, int itemsPerPage, int totalItems, IList<T> data)
        {
            Page = page;
            TotalPages = totalPages;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            Data = data;
        }

        public PagedResult<TDest> MapTo<TDest>(IMapper mapper)
        {
            return new PagedResult<TDest>(Page, TotalPages, ItemsPerPage, TotalItems, mapper.Map<IList<TDest>>(Data));
        }
    }
}
