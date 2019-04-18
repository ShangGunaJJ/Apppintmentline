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
using System.Net.Http;
using Ace;
using Chloe.Application.Interfaces.System;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    [LoginAuthorizeAttribute]
    public class PlaceInfoController : WebController
    {
        // GET: Appointment/PlaceInfo
        public ActionResult Index()
        {
            List<SelectOption> dutyModelList = SelectOption.CreateList(this.CreateService<IUserAppService>().GetSimpleModels());
            this.ViewBag.UserList = dutyModelList;
            return View();
        }
        [HttpPost]
        public ActionResult AddPeriod(AddPlaceInfoInput ti)
        {
            if (!this.CreateService<IPlaceInfoService>().IsAdd(ti.PlaceName, ti.Code)) return this.JsonContent(1);
            if (this.CreateService<IPlaceInfoService>().Add(ti).Id != "")
            {
                return this.AddSuccessMsg();
            }
            return this.AddSuccessMsg();
        }

        [HttpGet]
        public ActionResult GetModels(Pagination pagination, string keyword)
        {
            PagedData<PlaceInfo> pagedData = this.CreateService<IPlaceInfoService>().GetPageData(pagination, keyword);
            return this.SuccessData(pagedData);
        }

        [HttpPost]
        public ActionResult Update(UpdatePlaceInfoInput input)
        {
            if (!this.CreateService<IPlaceInfoService>().IsAdd(input.PlaceName, input.Code)) return this.JsonContent(1);
            this.CreateService<IPlaceInfoService>().Update(input);
            return this.UpdateSuccessMsg();
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            this.CreateService<IPlaceInfoService>().Delete(id);
            return this.DeleteSuccessMsg();
        }
    }
}