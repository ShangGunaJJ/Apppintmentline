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

            this.ViewBag.PerList = PerList;
            this.ViewBag.PlaceList = PlaceList;
            this.ViewBag.TranList = TranList;
            return View();
        }

        [HttpPost]
        public ActionResult AddBusiness(AddBusinessInput ti)
        {
            if (this.CreateService<IBusinessService>().Add(ti).Id != "")
            {
                return this.SuccessMsg();
            }
            return this.SuccessMsg("失败！");
        }

        [HttpGet]
        public ActionResult GetModels(Pagination pagination, string keyword)
        {
            PagedData<MALU_Business> pagedData = this.CreateService<IBusinessService>().GetPageData(pagination);
            return this.SuccessData(pagedData);
        }

    }
}