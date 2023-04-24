using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Core.Configration
{
    public class Client
    {
        //Istifade edecek mobil veya web [roqqram teminatina uygun 
        public string Id { get; set; }
        public string Secret { get; set; }

        //Hansi apilere icaze var, uygun kecidinine onu saxlayir bu 
        //www.mayapi1.com -na uygun isteklere icaze verir
        public List<String> Audiences { get; set; }
    }
}
