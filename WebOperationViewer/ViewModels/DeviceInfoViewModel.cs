using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOperationViewer.ViewModels
{
    public class DeviceInfoViewModel
    {
        public string TERM_ID { get; set; }
        public string TERM_NAME { get; set; }
        public string USED_SPACE { get; set; }
        public string FREE_SPACE { get; set; }
        public string STATUS { get; set; }
        public string DISK_USAGE { get; set; }
    }
}