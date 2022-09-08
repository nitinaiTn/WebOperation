using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOperationViewer.Models;
using WebOperationViewer.ViewModels;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Reflection;

namespace WebOperationViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly sladbEntities1 slaDB = new sladbEntities1();
        private readonly gsb_adm_fvEntities2 gsbDB = new gsb_adm_fvEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public  ActionResult TerminalInfo(String searchterminal, int? page, int? listpage)
        {
            int pageTemp = 1;
            int listPageTemp = 1000;

            if (Session["currentListPage"] == null)
            {
                Session["currentListPage"] = 1000;
            }
            if (page != null)
            {
                pageTemp = Convert.ToInt32(page);
            }
            if (listpage != null)
            {
                listPageTemp = Convert.ToInt32(listpage);
                Session["currentListPage"] = Convert.ToInt32(listpage);
            }
            else
            {
                listPageTemp = Convert.ToInt32(Session["currentListPage"].ToString());
            }
            if (searchterminal != null && page == null && listpage == null)
            {
                pageTemp = 1;
                listPageTemp = slaDB.devfwversion_record_.Where(s => s.TERM_ID.Contains(searchterminal)).ToList().Count();
            }
            SearchTerminal(searchterminal, pageTemp, listPageTemp);
            return View();   
        }

        public ActionResult SearchTerminal(String searchterminal , int page, int listpage)
        {
            if(String.IsNullOrEmpty(searchterminal))
            {
                int pagesize = listpage;

                int rowCount = slaDB.devfwversion_record_.Count();
                if(page < 1)
                {
                    page = 1;
                }

                var pager = new DataPager(rowCount, page, pagesize);
                int skipRows = (page - 1) * pagesize;
                ViewBag.Pager = pager;

                var deviceInfo = (from devinfo in gsbDB.device_info_
                                  select devinfo).ToList();

                var deviceFirmWare = (from devfw in slaDB.devfwversion_record_
                                      select devfw).ToList();

                var teminalInfo = (from devfw in deviceFirmWare
                                   join devinfo in deviceInfo
                                    on devfw.TERM_ID equals devinfo.TERM_ID
                                    select new FirmwareInfoViewModel{
                                        TERM_ID = devfw.TERM_ID,
                                        TERM_NAME = devinfo.TERM_NAME,
                                        CRM_VERSION = devfw.CRM_VERSION,
                                        PIN_VERSION = devfw.PIN_VERSION,
                                        IDC_VERSION = devfw.IDC_VERSION,
                                        PTR_VERSION = devfw.PTR_VERSION,
                                        BCR_VERSION = devfw.BCR_VERSION,
                                        SIU_VERSION = devfw.SIU_VERSION,
                                        UPDATE_DATE = devfw.UPDATE_DATE
                                    }).Skip(skipRows).Take(pagesize).ToList();
                return View(teminalInfo);
            }
            else
            {
                int pagesize = listpage;
                int rowCount = slaDB.devfwversion_record_.Count();
                if (page < 1)
                {
                    page = 1;
                }

                var pager = new DataPager(rowCount, page, pagesize);
                int skipRows = (page - 1) * pagesize;
                ViewBag.Pager = pager;

                var deviceInfo = (from devinfo in gsbDB.device_info_
                                  select devinfo).ToList();

                var deviceFirmWare = (from devfw in slaDB.devfwversion_record_
                                      select devfw).ToList();

                var teminalInfo = (from devfw in deviceFirmWare
                                   join devinfo in deviceInfo
                                    on devfw.TERM_ID equals devinfo.TERM_ID
                                   select new FirmwareInfoViewModel
                                   {
                                       TERM_ID = devfw.TERM_ID,
                                       TERM_NAME = devinfo.TERM_NAME,
                                       CRM_VERSION = devfw.CRM_VERSION,
                                       PIN_VERSION = devfw.PIN_VERSION,
                                       IDC_VERSION = devfw.IDC_VERSION,
                                       PTR_VERSION = devfw.PTR_VERSION,
                                       BCR_VERSION = devfw.BCR_VERSION,
                                       SIU_VERSION = devfw.SIU_VERSION,
                                       UPDATE_DATE = devfw.UPDATE_DATE
                                   }).Where(x => x.TERM_ID.Contains(searchterminal)).Skip(skipRows).Take(pagesize).ToList();

                return View(teminalInfo);
            }
        }

        public ActionResult LogConfig(string searchlogconfig, int? list365, int? page, int? listpage)//Controller LogCongfig
        {
            if (Session["listGen365"] == null)
            {
                Session["listGen365"] = 365;
            }
            if(searchlogconfig == null && list365 == null)
            {
                list365 = Convert.ToInt32(Session["listgen365"]);
            }
            else if(searchlogconfig != null && list365 != null)
            {
                Session["listGen365"] = list365;
            }
            else if (searchlogconfig != null && list365 == null)
            {

            }
            else if (searchlogconfig == null && list365 != null)
            {
                Session["listGen365"] = list365;
            }

            int pageTemp = 1;
            int listPageTemp = 1000;

            if (Session["currentListPage"] == null)
            {
                Session["currentListPage"] = 1000;
            }
            if (page != null)
            {
                pageTemp = Convert.ToInt32(page);
            }
            if (listpage != null)
            {
                listPageTemp = Convert.ToInt32(listpage);
                Session["currentListPage"] = Convert.ToInt32(listpage);
            }
            else
            {
                listPageTemp = Convert.ToInt32(Session["currentListPage"].ToString());
            }
            if (searchlogconfig != null && page == null && listpage == null)
            {
                pageTemp = 1;
                listPageTemp = slaDB.devfwversion_record_.Where(s => s.TERM_ID.Contains(searchlogconfig)).ToList().Count();
            }

            var LogInfo = (from loginfo in slaDB.logconfig_record_
                           select loginfo).ToList();
            var deviceInfo = (from devinfo in gsbDB.device_info_
                              select devinfo).ToList();

            

            if (String.IsNullOrEmpty(searchlogconfig) && list365 == null)
            {
                int pagesize = listPageTemp;

                int rowCount = slaDB.devfwversion_record_.Count();
                if (page < 1)
                {
                    page = 1;
                }

                var pager = new DataPager(rowCount, Convert.ToInt32(pageTemp), pagesize);
                int skipRows = (pageTemp - 1) * pagesize;
                ViewBag.Pager = pager;

                var LogConfigInfo = (from loginfo in LogInfo
                                     join devinfo in deviceInfo
                                     on loginfo.TERM_ID equals devinfo.TERM_ID
                                     select new LogConfigViewModel
                                     {
                                         TERM_ID = loginfo.TERM_ID,
                                         TERM_NAME = devinfo.TERM_NAME,
                                         CONFIG_DAY = loginfo.CONFIG_DAY,
                                         UPDATE_DATE = loginfo.UPDATE_DATE,
                                     }).Skip(skipRows).Take(pagesize).ToList();
                return View(LogConfigInfo);
            }
            else if (list365 != null)
            {

                var LogConfigInfo = (from loginfo in LogInfo
                                     join devinfo in deviceInfo
                                     on loginfo.TERM_ID equals devinfo.TERM_ID
                                     select new LogConfigViewModel
                                     {
                                         TERM_ID = loginfo.TERM_ID,
                                         TERM_NAME = devinfo.TERM_NAME,
                                         CONFIG_DAY = loginfo.CONFIG_DAY,
                                         UPDATE_DATE = loginfo.UPDATE_DATE,
                                     }).Where(x => x.CONFIG_DAY.ToString().Contains(list365.ToString())).ToList();
                return View(LogConfigInfo);
            }
            else
            {
                var LogConfigInfo = (from loginfo in LogInfo
                                     join devinfo in deviceInfo
                                     on loginfo.TERM_ID equals devinfo.TERM_ID
                                     select new LogConfigViewModel
                                     {
                                         TERM_ID = loginfo.TERM_ID,
                                         TERM_NAME = devinfo.TERM_NAME,
                                         CONFIG_DAY = loginfo.CONFIG_DAY,
                                         UPDATE_DATE = loginfo.UPDATE_DATE,
                                     }).Where(x => x.TERM_ID.Contains(searchlogconfig)).ToList();
                return View(LogConfigInfo);
            }
            
        }

        public ActionResult Partition(String searchpartition, int? page, int? listpage)//Controller Partition
        {
            int pageTemp = 1;
            int listPageTemp = 1000;

            if (Session["currentListPage"] == null)
            {
                Session["currentListPage"] = 1000;
            }
            if (page != null)
            {
                pageTemp = Convert.ToInt32(page);
            }
            if (listpage != null)
            {
                listPageTemp = Convert.ToInt32(listpage);
                Session["currentListPage"] = Convert.ToInt32(listpage);
            }
            else
            {
                listPageTemp = Convert.ToInt32(Session["currentListPage"].ToString());
            }
            if (searchpartition != null && page == null && listpage == null)
            {
                pageTemp = 1;
                listPageTemp = slaDB.devfwversion_record_.Where(s => s.TERM_ID.Contains(searchpartition)).ToList().Count();
            }


            var deviceInfo = (from devinfo in gsbDB.device_info_
                              select devinfo).ToList();

            var deviceInfoSys = (from devinfosys in gsbDB.device_info_system_
                                 select devinfosys.DISK_USAGE).ToList();

            var partitionInfo = (from devinfo in gsbDB.device_info_
                                 join devinfosys in gsbDB.device_info_system_
                                 on devinfo.TERM_ID equals devinfosys.TERM_ID
                                 select new DeviceInfoViewModel
                                 {
                                     TERM_ID = devinfosys.TERM_ID,
                                     TERM_NAME = devinfo.TERM_NAME,
                                     DISK_USAGE = devinfosys.DISK_USAGE,
                                 }).ToList();

            DataTable dt = ToDataTable(partitionInfo);

            List<DeviceInfoViewModel> partitionInfoAfterSpilt = new List<DeviceInfoViewModel>();

            if (String.IsNullOrEmpty(searchpartition))
            {
                int pagesize = listPageTemp;
                
                int rowCount = slaDB.devfwversion_record_.Count();
                if (page < 1)
                {
                    page = 1;
                }

                var pager = new DataPager(rowCount, Convert.ToInt32(pageTemp), pagesize);
                int skipRows = (pageTemp - 1) * pagesize;
                ViewBag.Pager = pager;

                partitionInfoAfterSpilt = (from DataRow dr in dt.Rows
                                           select new DeviceInfoViewModel
                                           {
                                               TERM_ID = dr["TERM_ID"].ToString(),
                                               TERM_NAME = dr["TERM_NAME"].ToString(),
                                               USED_SPACE = dr["USED_SPACE"].ToString(),
                                               FREE_SPACE = dr["FREE_SPACE"].ToString(),
                                               STATUS = dr["STATUS"].ToString()
                                           }).Skip(skipRows).Take(pagesize).ToList();
                return View(partitionInfoAfterSpilt);
            }
            else{

                int pagesize = listPageTemp;

                int rowCount = slaDB.devfwversion_record_.Count();
                if (page < 1)
                {
                    page = 1;
                }

                var pager = new DataPager(rowCount, Convert.ToInt32(pageTemp), pagesize);
                int skipRows = (pageTemp - 1) * pagesize;
                ViewBag.Pager = pager;

                partitionInfoAfterSpilt = (from DataRow dr in dt.Rows
                                           select new DeviceInfoViewModel
                                           {
                                               TERM_ID = dr["TERM_ID"].ToString(),
                                               TERM_NAME = dr["TERM_NAME"].ToString(),
                                               USED_SPACE = dr["USED_SPACE"].ToString(),
                                               FREE_SPACE = dr["FREE_SPACE"].ToString(),
                                               STATUS = dr["STATUS"].ToString()
                                           }).Where(x => x.TERM_ID.Contains(searchpartition)).Skip(skipRows).Take(pagesize).ToList();
                return View(partitionInfoAfterSpilt);
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    var tempSpilt = Props[5].GetValue(item, null).ToString().Split(',', ':', '/');
                    //inserting property values to datatable rows
                    if (i != 2 && i != 3 && i != 4) // Example Values[] = { TERM_ID , TERM_NAME, USED_SPACE, FREE_SPACE, STATUS}
                        values[i] = Props[i].GetValue(item, null);
                    if (tempSpilt[0] == "DiskF")
                    {
                        if (i == 2)
                            values[2] = tempSpilt[2];
                        else if (i == 3)
                            values[3] = Convert.ToInt32(tempSpilt[1]) - Convert.ToInt32(tempSpilt[2]);
                        else if (i == 4)
                        {
                            if (Convert.ToInt32(tempSpilt[1]) / 2 > Convert.ToInt32(tempSpilt[2]))
                                values[4] = "Normal";
                            else if(Convert.ToInt32(tempSpilt[1]) / 2 <= Convert.ToInt32(tempSpilt[2]))
                                values[4] = "Warning";
                        }         
                    }
                    else 
                    {
                        values[4] = "Drive F Missing";
                    }
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        public void ExportToExcelTerminal()//export terminal
        {
            var deviceInfo = (from devinfo in gsbDB.device_info_
                              select devinfo).ToList();

            var deviceFirmWare = (from devfw in slaDB.devfwversion_record_
                                  select devfw).ToList();

            var teminalInfo = (from devfw in deviceFirmWare
                               join devinfo in deviceInfo
                                on devfw.TERM_ID equals devinfo.TERM_ID
                               select new FirmwareInfoViewModel
                               {
                                   TERM_ID = devfw.TERM_ID,
                                   TERM_NAME = devinfo.TERM_NAME,
                                   CRM_VERSION = devfw.CRM_VERSION,
                                   PIN_VERSION = devfw.PIN_VERSION,
                                   IDC_VERSION = devfw.IDC_VERSION,
                                   PTR_VERSION = devfw.PTR_VERSION,
                                   BCR_VERSION = devfw.BCR_VERSION,
                                   SIU_VERSION = devfw.SIU_VERSION,
                                   UPDATE_DATE = devfw.UPDATE_DATE
                               }).ToList();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "TERM_ID";
            Sheet.Cells["B1"].Value = "TERM_NAME";
            Sheet.Cells["C1"].Value = "CRM_VERSION";
            Sheet.Cells["D1"].Value = "PIN_VERSION";
            Sheet.Cells["E1"].Value = "IDC_VERSION";
            Sheet.Cells["F1"].Value = "PTR_VERSION";
            Sheet.Cells["G1"].Value = "BCR_VERSION";
            Sheet.Cells["H1"].Value = "SIU_VERSION";
            Sheet.Cells["I1"].Value = "UPDATE_DATE";
            int row = 2;
            foreach (var item in teminalInfo)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.TERM_ID;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.TERM_NAME;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.CRM_VERSION;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.PIN_VERSION;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.IDC_VERSION;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.PTR_VERSION;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.BCR_VERSION;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.SIU_VERSION;
                Sheet.Cells[string.Format("I{0}", row)].Value = item.UPDATE_DATE;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public void ExportToExcelLogConfig()//export logConfig
        {
            var LogInfo = (from loginfo in slaDB.logconfig_record_
                           select loginfo).ToList();
            var deviceInfo = (from devinfo in gsbDB.device_info_
                              select devinfo).ToList();

            var LogConfigInfo = (from loginfo in LogInfo
                                 join devinfo in deviceInfo
                                 on loginfo.TERM_ID equals devinfo.TERM_ID
                                 select new LogConfigViewModel
                                 {
                                     TERM_ID = loginfo.TERM_ID,
                                     TERM_NAME = devinfo.TERM_NAME,
                                     CONFIG_DAY = loginfo.CONFIG_DAY,
                                     UPDATE_DATE = loginfo.UPDATE_DATE,
                                 }).ToList();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "TERM_ID";
            Sheet.Cells["B1"].Value = "TERM_NAME";
            Sheet.Cells["C1"].Value = "CONFIG_DAY";
            Sheet.Cells["D1"].Value = "UPDATE_DATE";
            int row = 2;
            foreach (var item in LogConfigInfo)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.TERM_ID;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.TERM_NAME;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.CONFIG_DAY;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.UPDATE_DATE;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
        

        public void ExportToExcelPartition()//export partition
        {
            var deviceInfo = (from devinfo in gsbDB.device_info_
                              select devinfo).ToList();

            var deviceInfoSys = (from devinfosys in gsbDB.device_info_system_
                                 select devinfosys.DISK_USAGE).ToList();

            var partitionInfo = (from devinfo in gsbDB.device_info_
                                 join devinfosys in gsbDB.device_info_system_
                                 on devinfo.TERM_ID equals devinfosys.TERM_ID
                                 select new DeviceInfoViewModel
                                 {
                                     TERM_ID = devinfosys.TERM_ID,
                                     TERM_NAME = devinfo.TERM_NAME,
                                     DISK_USAGE = devinfosys.DISK_USAGE,
                                 }).ToList();

            DataTable dt = ToDataTable(partitionInfo);
            List<DeviceInfoViewModel> partitionInfoAfterSpilt = new List<DeviceInfoViewModel>();
            partitionInfoAfterSpilt = (from DataRow dr in dt.Rows
                                       select new DeviceInfoViewModel
                                       {
                                           TERM_ID = dr["TERM_ID"].ToString(),
                                           TERM_NAME = dr["TERM_NAME"].ToString(),
                                           USED_SPACE = dr["USED_SPACE"].ToString(),
                                           FREE_SPACE = dr["FREE_SPACE"].ToString(),
                                           STATUS = dr["STATUS"].ToString()
                                       }).ToList();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "TERM_ID";
            Sheet.Cells["B1"].Value = "TERM_NAME";
            Sheet.Cells["C1"].Value = "USED_SPACE";
            Sheet.Cells["D1"].Value = "FREE_SPACE";
            Sheet.Cells["E1"].Value = "STATUS";
            int row = 2;
            foreach (var item in partitionInfoAfterSpilt)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.TERM_ID;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.TERM_NAME;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.USED_SPACE;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.FREE_SPACE;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.STATUS;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

    }
}