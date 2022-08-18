using SmsManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsManagementSystem.ViewModel
{
    public class GroupMessageVM
    {
        public IEnumerable<Draft> DraftList { get; set; }
        public Draft Draft { get; set; }

        public IEnumerable<CgppOffice> OfficeList { get; set; }
        public CgppOffice Office { get; set; }
        public IEnumerable<CgppPhonebook> PhonebookList { get; set; }
        public CgppPhonebook Phonebook { get; set; }
        public IEnumerable<CgppGroup> GroupList { get; set; }
        public CgppGroup Group { get; set; }
        public int GroupId { get; set; }
        public int PhoneId { get; set; }
    }
}