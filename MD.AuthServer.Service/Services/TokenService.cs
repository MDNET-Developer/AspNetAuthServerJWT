using MD.AuthServer.Core.Configration;
using MD.AuthServer.Core.Dto;
using MD.AuthServer.Core.Model;
using MD.AuthServer.Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLiblary.Confuguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOptions _customTokenOptions;

        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOptions> options)
        {
            _userManager = userManager;
            _customTokenOptions = options.Value;
        }

        private string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            throw new NotImplementedException();
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
