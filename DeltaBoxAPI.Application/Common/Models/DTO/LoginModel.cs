﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Common.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string? UsernameOrEmail { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
