using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLiblary.Confuguration
{
    public class CustomTokenOptions
    {
        public List<String> Audence{ get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }  
        public int RefreshTokenExpiration { get; set; }  
        public string SecurityKey { get; set; }  
    }
}
