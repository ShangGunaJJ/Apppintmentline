using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chloe.Application.Models.Appointment;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Admin.Common;
using Ace;
using Chloe.Application.Interfaces.System;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class EvaController : WebController
    {
        // GET: Appointment/Eva
        public ActionResult Index()
        {
            List<SelectOption> TranList = SelectOption.CreateList(this.CreateService<ITransactionInfoService>().GetPerSimple());

            this.ViewBag.TranList = TranList;
            return View();
        }

        public ActionResult GetModels(Pagination pagination, string keyword, string TransactionID, string AppDate, int Star) {
            var IAppService = this.CreateService<IEvaluateService>();
            PagedData<SelEvaluate> pagedData = IAppService.GetPageData(pagination, keyword, TransactionID, AppDate, Star);
            return this.SuccessData(pagedData);
        }
    }
}