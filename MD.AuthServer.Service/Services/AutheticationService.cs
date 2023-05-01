using MD.AuthServer.Core.Configration;
using MD.AuthServer.Core.Dto;
using MD.AuthServer.Core.Model;
using MD.AuthServer.Core.Repositries;
using MD.AuthServer.Core.Service;
using MD.AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLiblary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Service.Services
{
    public class AutheticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitofwork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AutheticationService(IOptions<List<Client>> clients, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitofwork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = clients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitofwork = unitofwork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<Response<TokenDto>> CreateToken(LogInDto logIn)
        {
            if (logIn == null)
                throw new ArgumentNullException(nameof(logIn));
            

            var user = await _userManager.FindByEmailAsync(logIn.Email);
            if(user == null) 
                return Response<TokenDto>.Fail("Email or Password not correct", 400, true);
            
            if((await _userManager.CheckPasswordAsync(user,password:logIn.Password))==false)
                return Response<TokenDto>.Fail("Email or Password not correct", 400, true);
            


            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if(userRefreshToken == null)
               await _userRefreshTokenService.AddAsync(new UserRefreshToken()
                {
                    UserId=user.Id,
                    Code=token.RefreshToken,
                    Expiration=token.RefreshTokenExpiration
                });

            else
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            
            await _unitofwork.CommitAsync();
            return Response<TokenDto>.Success(token, 200);
        }

        public Task<Response<ClientLogInDto>> CreateTokenByClient(ClientLogInDto clientLogInDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
