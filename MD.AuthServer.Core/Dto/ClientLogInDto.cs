using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Core.Dto
{
    public class ClientLogInDto
    {
        public string ClientId { get; set; }//username
        public string ClientSecret { get; set; }//password
    }
}
