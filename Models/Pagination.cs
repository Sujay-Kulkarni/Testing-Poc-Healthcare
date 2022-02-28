using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testing_Poc_Healthcare.Interface;

namespace Testing_Poc_Healthcare.Models
{
    public class Pagination : IPagination
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
