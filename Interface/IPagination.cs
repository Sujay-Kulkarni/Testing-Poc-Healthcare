using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing_Poc_Healthcare.Interface
{
   public interface IPagination
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
