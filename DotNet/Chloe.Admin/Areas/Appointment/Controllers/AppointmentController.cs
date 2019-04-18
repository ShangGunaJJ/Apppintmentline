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
    public class AppointmentController : WebController
    {
        // GET: Appointment/Appointment
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
        public ActionResult AddAppointment(AddAppointmentDataInput ti)
        {
            if (this.CreateService<IAppointmentDataService>().Add(ti).Id != "")
            {
                return this.SuccessMsg();
            }
            return this.SuccessMsg("失败！");
        }
        [HttpGet]
        public ActionResult GetModels(Pagination pagination, string keyword, string TransactionID, string PlaceId, string PeriodTimeID,string AppDate,int state)
        {
            var IAppService= this.CreateService<IAppointmentDataService>();
            PagedData<SelAppointmentData> pagedData = IAppService.GetPageData(pagination, keyword, TransactionID, PlaceId, PeriodTimeID, AppDate, state);
           
            WebService.WebService ws = new WebService.WebService();
            foreach (var view in pagedData.DataList)
            {
                try
                {
                    string id = view.Id;
                    DateTime day = view.AppointmentDate.Value;
                    string pertime = view.PeriodTime;
                    DateTime dt = day.AddHours(Int32.Parse(pertime.Split('-')[0].Split(':')[0])).AddMinutes(Int32.Parse(pertime.Split('-')[0].Split(':')[1]));
                    DateTime dt2 = day.AddHours(Int32.Parse(pertime.Split('-')[1].Split(':')[0])).AddMinutes(Int32.Parse(pertime.Split('-')[1].Split(':')[1]));

                    if (view.State != 2 && view.State != -1 && view.State != -2)
                    {
                        int IsValid = ws.IsValid(view.BookingNo);
                        if (IsValid > 0)
                        {
                            string mID = view.MamberID;
                            string CodeID = view.Id;
                            string IDCard = this.CreateService<IMembersAppService>().GetIDCard(mID);
                            if (DateTime.Now >= dt && DateTime.Now <= dt2)
                            {
                                view.State = 1;
                                view.MALU_Code = ws.GetTicketCode(view.SerNo, IDCard);
                                IAppService.UpdateCode(CodeID, view.MALU_Code);
                            }
                            else if (DateTime.Now > dt2)
                            {
                                view.State = 2;
                                if (string.IsNullOrEmpty(view.MALU_Code)) view.MALU_Code = "";
                                IAppService.UpdataState(id, 2);
                            }
                        }
                        else if (DateTime.Now > dt2)
                        {
                            view.State = -2;
                            view.MALU_Code = "";
                            IAppService.UpdataState(id, -2);
                        }
                        else if (DateTime.Now < dt)
                        {
                            view.State = 0;
                            view.MALU_Code = "";
                            IAppService.UpdataState(id, 0);
                        }
                        else if (DateTime.Now >= dt && DateTime.Now <= dt2)
                        {
                            view.State = 1;
                            view.MALU_Code = "";
                            IAppService.UpdataState(id, 1);
                        }
                    }
                }
                catch {
                }
            }
            return this.SuccessData(pagedData);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            this.CreateService<IAppointmentDataService>().Delete(id);
            return this.DeleteSuccessMsg();
        }
    }
}