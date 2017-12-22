using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chloe.Application.Models.Appointment;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Entities;
using Chloe.Admin.Common;
using Ace;
using Chloe.Application.Interfaces.System;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class TranController : WebController
    {
        // GET: Appointment/Tran
        public ActionResult Index()
        {
            List<SelectOption> dutyModelList = SelectOption.CreateList(this.CreateService<IUserAppService>().GetSimpleModels());
            this.ViewBag.UserList = dutyModelList;
            return View();
        }
        [HttpPost]
        public ActionResult Add(AddTransactionInfoInput ti) {
            if (this.CreateService<ITransactionInfoService>().Add(ti).Id != "") {
                return this.AddSuccessMsg();
            }
            return this.AddSuccessMsg();
        }

        [HttpGet]
        public ActionResult GetModels(Pagination pagination, string keyword)
        {
            PagedData<TransactionInfo> pagedData = this.CreateService<ITransactionInfoService>().GetPageData(pagination, keyword);
            return this.SuccessData(pagedData);
        }

        [HttpPost]
        public ActionResult Update(UpdateTransactionInfoInput input)
        {
            this.CreateService<ITransactionInfoService>().Update(input);
            return this.UpdateSuccessMsg();
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            this.CreateService<ITransactionInfoService>().Delete(id);
            return this.DeleteSuccessMsg();
        }
    }
}