﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Banner
{
    public class CommonImageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string IsActive { get; set; }
    }
}