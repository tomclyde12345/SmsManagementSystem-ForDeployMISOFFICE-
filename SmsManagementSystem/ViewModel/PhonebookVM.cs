using SmsManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsManagementSystem.ViewModel
{
    public class PhonebookVM
    {
        public IEnumerable<CgppPhonebook> PhonebookList { get; set; }
        public CgppPhonebook Phonebook { get; set; }
        public IEnumerable<CgppGroup> GroupList { get; set; }
        public CgppGroup Group { get; set; }
        public IEnumerable<Draft> DraftList { get; set; }
        public Draft Draft { get; set; }

        public int GroupId { get; set; }
    }
}