using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsManagementSystem.ViewModel
{
    public class DraftListVM
    {
        public string msg { get; set; }
        public int draftId { get; set; }
        public string oficeName { get; set; }
        public string divisionName { get; set; }
        public int? tag { get; set; }
        public string username { get; set; }
        public DateTime? Dateadded { get; set; }
        public string cellphonenum { get; set; }
        public string sendToName { get; set; }
    }
}