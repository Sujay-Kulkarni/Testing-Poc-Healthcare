using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing_Poc_Healthcare.Models
{
    public class JwtInfo
    {
        public string Jwtkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
