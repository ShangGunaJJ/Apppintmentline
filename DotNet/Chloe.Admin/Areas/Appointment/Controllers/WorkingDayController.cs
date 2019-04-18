using Ace.IdStrategy;
using Chloe.Admin.Common.Tree;
using Chloe.Application.Interfaces;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Admin.Common;
using Chloe.Entities;
using Chloe.Application.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Timers;
using Ace.VerifyCode;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Configuration;
using Chloe.Entities.Enums;
using System.Web.Security;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class WorkingDayController : WebController
    {
        // GET: Appointment/WorkingDay
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ContentResult getData() {
            List<WorkingDay> data = this.CreateService<IWorkingDayService>().Select();
            return this.JsonContent(data);
        }
        [HttpPost]
        public ContentResult delete(string date) {
            int c = this.CreateService<IWorkingDayService>().Delete(DateTime.Parse(date));
            return this.JsonContent(c);
        }

        [HttpPost]
        public ContentResult Set(string date)
        {
            AddWorkingDayInput w = new AddWorkingDayInput();
            w.Date = DateTime.Parse(date);
            w.isWorkingDay = 1;
            WorkingDay c = this.CreateService<IWorkingDayService>().Add(w);
            return this.JsonContent(c);
        }
    }
}