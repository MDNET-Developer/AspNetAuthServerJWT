using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLiblary.Dtos
{
    public class ErrorDto
    {
        public List<String> Errors { get; set; }
        public bool IsShow { get; set; }/*Developerin basa duseceyi xetalar olacaqsa false, clientin(istifadecinin goreceyi xeta olacaqsa true edeceyik)*/

        public ErrorDto() { Errors = new List<string>(); }
        public ErrorDto(string error,bool isShow)
        {
            Errors.Add(error);
            isShow = true;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
