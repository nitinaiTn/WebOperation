using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebOperationViewer.Models;

namespace WebOperationViewer.ViewModels
{
    public class TerminalViewModel
    {
        public IEnumerable<logconfig_record_> logconfig_Record_s { get; set; }
        public IEnumerable<devfwversion_record_> devfwversion_record_s { get; set; }
        public IEnumerable<device_info_> device_Info_s { get; set; }
        public IEnumerable<device_info_system_> device_Info_System_s { get; set; }

    }
}