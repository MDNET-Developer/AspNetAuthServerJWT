using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Core.Dto
{
    public class ClientTokenDto
    {
        public string AccsessToken { get; set; }
        public DateTime AcccsessTokenExpiration { get; set; }
        


    }
}
