using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLiblary.Dtos
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        public ErrorDto Error { get; set; }

        public static Response<T> Success(T data,int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode,IsSuccessful=true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> {StatusCode = statusCode, IsSuccessful=true };
        }

        public static Response<T> Fail(ErrorDto errorDto, int statusCode)
        {
            return new Response<T> { Error=errorDto, StatusCode = statusCode, IsSuccessful=false };
        }

        public static Response<T> Fail(string errorMessage, int statusCode,bool isShow)
        {
            return new Response<T> { Error = new ErrorDto(errorMessage,isShow), StatusCode = statusCode, IsSuccessful=false };
        }
    }
}
