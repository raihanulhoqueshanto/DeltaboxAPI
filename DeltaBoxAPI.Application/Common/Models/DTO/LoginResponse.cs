using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Common.Models.DTO
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken {  get; set; }
        public DateTime? Expiration { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
    }
}
