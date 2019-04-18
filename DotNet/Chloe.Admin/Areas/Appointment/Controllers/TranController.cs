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
            if (this.CreateService<ITransactionInfoService>().IsAddOrUpdate(ti.TransactionName, ti.Code)>0) return this.JsonContent(1);
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
            if (this.CreateService<ITransactionInfoService>().IsAddOrUpdate(input.TransactionName, input.Code)>1) return this.JsonContent(1);
            this.CreateService<ITransactionInfoService>().Update(input);
            return this.UpdateSuccessMsg();
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            this.CreateService<ITransactionInfoService>().Delete(id);
            return this.DeleteSuccessMsg();
        }

        [HttpGet]
        public ActionResult CloneData()
        {
            WebService.WebService ww = new WebService.WebService();
            var obj = ww.GetSer();
            List<int> SerNos = new List<int>();
            var _ser = this.CreateService<ITransactionInfoService>();
            foreach (var a in obj) {
                SerNos.Add(a.ServiceNo);
                if (_ser.IsAre(a.ServiceNo) > 0)
                {
                    _ser.SerUpdate(a.ServiceNo, a.ServiceName, a.ServiceType);
                }
                else {
                    _ser.AddData(a.ServiceNo, a.ServiceName, a.ServiceType);
                }
            }
            _ser.DeleteDate(SerNos);
            return this.SuccessData();
        }
    }
    public class ServiceInput
    {
        public int ServiceNo { get; set; }
        public string ServiceName { get; set; }

        public string ServiceType { get; set; }
    }
}