using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmsManagementSystem.Models;
namespace SmsManagementSystem.ViewModel
{
    public class ManageVM
    {
        //public IEnumerable<Login> LoginList { get; set; }
        public IEnumerable<CgppOffice> OfficeList { get; set; }
        public IEnumerable<CgppDivision> DivisionList { get; set; }
        public IEnumerable<Login> Loginslist { get; set; }
        //public IEnumerable<User> UsersList { get; set; }
        //public User Users { get; set; }
        //public Login Logins { get; set; }

        // public CgppOffice Offices { get; set; }

        // public CgppDivision Division { get; set; }


        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public int Officeid { get; set; }
        public int Divisionid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
        public HttpPostedFileBase[] files { get; set; }
        public string FilePath { get; set; }
    }
}