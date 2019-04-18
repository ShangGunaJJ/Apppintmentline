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
    public class MemberController : WebController
    {
        // GET: Appointment/Member
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetModels(Pagination pagination, string keyword,int? state=0)
        {
            PagedData<MALU_Members> pagedData = this.CreateService<IMembersAppService>().GetPageData(pagination, keyword,state);
            return this.SuccessData(pagedData);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            this.CreateService<IMembersAppService>().Delete(id, -1);
            return this.DeleteSuccessMsg();
        }
        [HttpPost]
        public ActionResult Update(string id)
        {
            this.CreateService<IMembersAppService>().Delete(id,1);
            return this.DeleteSuccessMsg();
        }
    }
}