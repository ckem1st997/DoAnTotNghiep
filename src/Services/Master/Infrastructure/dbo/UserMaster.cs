﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Share.Base.Core;

using System;
using System.Collections.Generic;


namespace Infrastructure
{
    public partial class UserMaster:BaseEntity
    {


        public string UserName { get; set; } = default!;


        public string Password { get; set; } = default!;


        public bool? InActive { get; set; }


        public string Role { get; set; } = default!;


        public int RoleNumber { get; set; }


        public long? Phone { get; set; }


        public string? FullName { get; set; }


        public DateTime? ModifyDate { get; set; }


        public string? ModifyBy { get; set; }


        public DateTime? CreateDate { get; set; }


        public string? CreateBy { get; set; }
    }
}