using AutoMapper;
using MD.AuthServer.Core.Dto;
using MD.AuthServer.Core.Model;
using MD.AuthServer.Core.Service;
using Microsoft.AspNetCore.Identity;
using SharedLiblary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly IMapper _mapper;
        public UserService(UserManager<UserApp> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp() 
            { 
            Email= createUserDto.Email,
            UserName=createUserDto.Username
            };

            var result = await _userManager.CreateAsync(user,createUserDto.Password);
            if(!result.Succeeded)
            {

               var errors = result.Errors.Select(x=>x.Description).ToList();
                return Response<UserAppDto>.Fail(new ErrorDto(errors,true), 400);
            }

            var mappedData = _mapper.Map<UserAppDto>(user);
            return Response<UserAppDto>.Success(mappedData, 200);
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user =  await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return Response<UserAppDto>.Fail("UserName not found", 404, true);
            }

            var mappedData = _mapper.Map<UserAppDto>(user);
            return Response<UserAppDto>.Success(mappedData, 200);
        }
    }
}
