﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public partial class HistoryNotication:BaseEntity
    {
        public string UserName { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Read { get; set; }
        public string Link { get; set; }
        public string UserNameRead { get; set; }
    }
}