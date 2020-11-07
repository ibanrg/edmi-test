using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Data.Models.Responses
{
    public class BaseResponse
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }

        public BaseResponse()
        {
            Valid = true;
        }
    }
}