using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOperationViewer.ViewModels
{
    public class LogConfigViewModel
    {
        public string TERM_ID { get; set; }
        public string TERM_NAME { get; set; }
        public Nullable<double> CONFIG_DAY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }

    }
}