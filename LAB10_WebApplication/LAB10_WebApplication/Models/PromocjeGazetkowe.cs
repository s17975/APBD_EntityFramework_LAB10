﻿using System;
using System.Collections.Generic;

namespace LAB10_WebApplication.Models
{
    public partial class PromocjeGazetkowe
    {
        public string SiecHandlowa { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public string FormatGazetki { get; set; }
    }
}
