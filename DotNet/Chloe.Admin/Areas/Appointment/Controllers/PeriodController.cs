using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chloe.Application.Models.Appointment;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Admin.Common;
using Chloe.Entities;
using Ace;
using Chloe.Application.Interfaces.System;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class PeriodController : WebController
    {
        // GET: Appointment/Period
        public ActionResult Index()
        {
            List<SelectOption> dutyModelList = SelectOption.CreateList(this.CreateService<IUserAppService>().GetSimpleModels());
            this.ViewBag.UserList = dutyModelList;
            return View();
        }

        [HttpPost]
        public ActionResult AddPeriod(AddPeriodTimeInput ti)
        {
            if (this.CreateService<IPeriodTime>().IsOKPeriod(ti))
            {
                if (this.CreateService<IPeriodTime>().Add(ti).Id != "")
                {
                    return this.AddSuccessMsg();
                }
                return this.SuccessMsg("失败！");
            }
            else {
                return this.SuccessMsg("失败！时间段重复存在！");
            }
        }
        [HttpGet]
        public ActionResult GetModels(Pagination pagination, string keyword)
        {
            PagedData<PeriodTime> pagedData = this.CreateService<IPeriodTime>().GetPageData(pagination, keyword);
            return this.SuccessData(pagedData);
        }

        [HttpPost]
        public ActionResult Update(UpdatePeriodTimeInput input)
        {
            AddPeriodTimeInput ti = new AddPeriodTimeInput() { StratTime=input.StratTime, EndTime=input.EndTime, SeveraWeeks=input.SeveraWeeks };
            if (this.CreateService<IPeriodTime>().IsOKPeriod(ti))
            {
                this.CreateService<IPeriodTime>().Update(input); return this.UpdateSuccessMsg();
            }
            else {

                return this.SuccessMsg("失败！时间段重复存在！");
            } 
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            this.CreateService<IPeriodTime>().Delete(id);
            return this.DeleteSuccessMsg();
        }
    }
}