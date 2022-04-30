using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyblock.Common.Interfaces
{
    public interface IPageable
    {
        public int Page { get; set; }
        public int AuctionsPerPage { get; set; }
    }
}
