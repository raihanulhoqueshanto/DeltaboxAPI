using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Common
{
    public class TokenInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
