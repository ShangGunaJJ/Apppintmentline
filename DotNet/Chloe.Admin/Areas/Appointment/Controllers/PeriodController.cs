using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chloe.Application.Models.Appointment;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Admin.Common;
using Chloe.Entities;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class PeriodController : WebController
    {
        // GET: Appointment/Period
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPeriod(AddPeriodTimeInput ti)
        {
            if (this.CreateService<IPeriodTime>().Add(ti).Id != "")
            {
                return this.SuccessMsg();
            }
            return this.SuccessMsg("失败！");
        }
        [HttpGet]
        public ActionResult GetModels() {           
            List<PeriodTime> data = null;
            this.SuccessData(data);
            return null;
        }
    }
}