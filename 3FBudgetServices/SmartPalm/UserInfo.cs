//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartPalm
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string MiddleName { get; set; }
        public string ContactNumber { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public Nullable<int> TabletId { get; set; }
        public string UserCode { get; set; }
        public string EmployeeCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedByUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool IsApplicationAccess { get; set; }
    }
}