using Ace.IdStrategy;
using Chloe.Application.Interfaces;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using Chloe.Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ace;
using Chloe.Application.Interfaces.System;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class BusinessController : WebController
    {
        // GET: Appointment/Business
        public ActionResult Index()
        {
            List<SelectOption> PerList = SelectOption.CreateList(this.CreateService<IPeriodTime>().GetPerSimple());

            List<SelectOption> PlaceList = SelectOption.CreateList(this.CreateService<IPlaceInfoService>().GetSimple());
            List<SelectOption> TranList = SelectOption.CreateList(this.CreateService<ITransactionInfoService>().GetPerSimple());
            List<SelectOption> dutyModelList = SelectOption.CreateList(this.CreateService<IUserAppService>().GetSimpleModels());
            this.ViewBag.PerList = PerList;
            this.ViewBag.PlaceList = PlaceList;
            this.ViewBag.TranList = TranList;
            this.ViewBag.UserList = dutyModelList;
            return View();
        }

        [HttpPost]
        public ActionResult AddBusiness(AddBusinessInput ti)
        {
            var IsRe = this.CreateService<IBusinessService>().IsAddSer(ti.PeriodTimeID, ti.PlaceId, ti.TransactionID, "");
            if (IsRe > 0)
                return this.JsonContent(IsRe);
            WebService.WebService ss = new WebService.WebService();

            PeriodTime per = this.CreateService<IPeriodTime>().Select(ti.PeriodTimeID)[0];
            TransactionInfo tt = this.CreateService<ITransactionInfoService>().GetPerByID(ti.TransactionID)[0];

            //int SerID = ss.AddService(tt.Code,tt.TransactionName,"","",DateTime.Parse(DateTime.Now.ToShortDateString()+" "+per.StratTime),DateTime.Parse(DateTime.Now.ToShortDateString() + " " + per.EndTime),ti.AppointmentNum, DateTime.Parse(DateTime.Now.ToShortDateString() + " " + per.StratTime),DateTime.Parse(DateTime.Now.ToShortDateString() + " " + per.EndTime), ti.AppointmentNum);
            //ti.ServiceNo = SerID;
            if (this.CreateService<IBusinessService>().Add(ti).Id != "")
            {
                return this.SuccessMsg();
            }
            return this.SuccessMsg("失败！");
        }

        [HttpGet]
        public ActionResult GetModels(Pagination pagination, string TransactionID, string PlaceId, string PeriodTimeID)
        {
            PagedData<MALU_Business> pagedData = this.CreateService<IBusinessService>().GetPageData(pagination, TransactionID,  PlaceId,  PeriodTimeID);
            return this.SuccessData(pagedData);
        }
        [HttpPost]
        public ActionResult Update(UpdateBusinessInput input)
        {
            var IsRe = this.CreateService<IBusinessService>().IsAddSer(input.PeriodTimeID, input.PlaceId, input.TransactionID, input.Id);
            if (IsRe > 0)
                return this.JsonContent(IsRe);
            this.CreateService<IBusinessService>().Update(input);
            return this.UpdateSuccessMsg();
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            this.CreateService<IBusinessService>().Delete(id);
            return this.DeleteSuccessMsg();
        }
    }
}