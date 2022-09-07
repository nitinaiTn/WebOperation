using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOperationViewer.ViewModels
{
    public class FirmwareInfoViewModel
    {
        public string ID { get; set; }
        public string TERM_ID { get; set; }
        public string TERM_NAME { get; set; }
        public string CRM_VERSION { get; set; }
        public string PIN_VERSION { get; set; }
        public string IDC_VERSION { get; set; }
        public string PTR_VERSION { get; set; }
        public string BCR_VERSION { get; set; }
        public string SIU_VERSION { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }
}