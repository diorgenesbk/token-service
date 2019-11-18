using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Token.DataObject.Response
{
    public class TokenResponseDto
    {
        public bool Authenticated { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string AccessToken { get; set; }

        public string Message { get; set; }
    }
}
