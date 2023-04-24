using MD.AuthServer.Core.Configration;
using MD.AuthServer.Core.Dto;
using MD.AuthServer.Core.Model;

namespace MD.AuthServer.Core.Service
{
    public interface ITokenService
    {
        //Kenar dunyaya acmayacam men bu kodu. Oz proyekt daxilimde isledecem deyene Response<> dan istifade etmedim
        TokenDto CreateToken(UserApp userApp);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
