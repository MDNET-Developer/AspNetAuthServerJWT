using MD.AuthServer.Core.Dto;
using SharedLiblary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.AuthServer.Core.Service
{
    public interface IUserService
    {
        //Yeni istifadecileri yadda saxlayacaq
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
    }
}
