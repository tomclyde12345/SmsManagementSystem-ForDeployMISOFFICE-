using SmsManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmsManagementSystem.ViewModel
{
    public class DraftVM
    {
        public IEnumerable<Draft> DraftList { get; set; }
        public Draft Draft { get; set; }
        public IEnumerable<CgppPhonebook> Phonebooklist { get; set; }
        public CgppPhonebook Phonebooks { get; set; }
        public List<string> sendTo { get; set; }
        public string msg { get; set; }
        public string tag { get; set; }
        public int draftId { get; set; }
        public List<CgppPhonebook> CgppPhonebookList { get; set; }
        public List<CgppDraft> CgppDraftkList { get; set; }
        public CgppDraft CgppDraft { get; set; }

    }
}