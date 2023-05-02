using MD.AuthServer.Core.Dto;
using SharedLiblary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Core.Service
{
    public interface IAuthenticationService
    {
        //Bu service bir basa Api ile xeberlesecek servisdir
        Task<Response<TokenDto>> CreateToken(LogInDto logIn); //Eger bu loginDto dogrudursa geriye Token donecek
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);
        Response<ClientTokenDto> CreateTokenByClient(ClientLogInDto clientLogInDto);
    }
}
