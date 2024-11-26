using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Common
{
    [Table("password_reset_otp")]
    public class PasswordResetOTP
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("otp")]
        public string OTP { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }
    }
}
