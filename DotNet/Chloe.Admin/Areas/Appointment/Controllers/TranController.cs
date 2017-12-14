using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chloe.Application.Models.Appointment;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Entities;
using Chloe.Admin.Common;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class TranController : WebController
    {
        // GET: Appointment/Tran
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AddTransactionInfoInput ti) {
            if (this.CreateService<ITransactionInfoService>().Add(ti).Id != "") {
                return this.AddSuccessMsg();
            }
            return this.AddSuccessMsg();
        }

        public List<TransactionInfo> GetModels()
        {
            List<TransactionInfo> tl = new List<TransactionInfo>();
            return tl;
        }
    }
}