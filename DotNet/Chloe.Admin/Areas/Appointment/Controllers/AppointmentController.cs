using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chloe.Application.Models.Appointment;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Admin.Common;
namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class AppointmentController : WebController
    {
        // GET: Appointment/Appointment
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAppointment(AddAppointmentDataInput ti)
        {
            if (this.CreateService<IAppointmentDataService>().Add(ti).Id != "")
            {
                return this.SuccessMsg();
            }
            return this.SuccessMsg("失败！");
        }
    }
}