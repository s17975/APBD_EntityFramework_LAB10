using System;
using System.Collections.Generic;

namespace LAB10_WebApplication.Models
{
    public partial class Roles
    {
        public string IndexNumber { get; set; }
        public string DbRole { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
