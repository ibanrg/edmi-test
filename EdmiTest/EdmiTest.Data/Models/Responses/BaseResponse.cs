using System;
using System.Collections.Generic;
using System.Text;

namespace EdmiTest.Data.Models.Responses
{
    public class BaseResponse
    {
        public bool Valid { get; set; }
        public List<string> ErrorMessages { get; set; }

        public BaseResponse()
        {
            Valid = true;
            ErrorMessages = new List<string>();
        }
    }
}