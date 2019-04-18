using Ace;
using Ace.IdStrategy;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.Appointment
{
    public  class AppointmentDataService: AdminAppService,IAppointmentDataService
    {
        public string Delete(string id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<AppointmentData>(a => id==a.Id);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue  + "条。";
        }

        public int UpdateStop(string ID) {

            if (this.DbContext.Update<AppointmentData>(a => a.Id == ID, a => new AppointmentData()
            {
                State = -1
            }) > 0)
            {
                return 1;
            }
            return 0;
        }

        public AppointmentData Add(AddAppointmentDataInput input)
        {
            AppointmentData entity = new AppointmentData();
            string _Code = (this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == input.BusinessID).Select(a => AggregateFunctions.Count()).First()+1).ToString();
            while (_Code.Length < 4) {
                _Code = "0" + _Code;
            }
            entity.Id = IdHelper.CreateGuid();
            entity.CreateTime = DateTime.Now;
            entity.AppointmentDate = input.AppointmentDate;
            entity.BusinessID = input.BusinessID;
            entity.FileID = input.FileID;
            //entity.MALU_Code = input.Code + _Code;
            entity.MamberID =  input.MamberID;
            entity.BookingNo = input.BookingNo;
            entity.State = input.State;
            return this.DbContext.Insert(entity);
        }

        public int Update(UpdateAppointmentDataInput input)
        {

            if (this.DbContext.Update<AppointmentData>(a => a.Id == input.Id, a => new AppointmentData()
            {
                AppointmentDate = input.AppointmentDate,
                BusinessID = input.BusinessID,
                FileID = input.FileID,
                MALU_Code = input.MALU_Code,
                MamberID = input.MamberID,
                State = input.State
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
        public int UpdateCode(string id,string code)
        {

            if (this.DbContext.Update<AppointmentData>(a => a.Id == id, a => new AppointmentData()
            {
                MALU_Code = code,
                State =1
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
        public int SelectAppCount(string BusID) {
           return this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == BusID).ToList().Count();
        }
        public int Eval(string appID) {
            //if (this.DbContext.Update<AppointmentData>(a => a.Id == appID, a => new AppointmentData()
            //{
            //     IsEvaluate=1
            //}) > 0)
            //{
            //    return 1;
            //}
            return 0;
        }
        public void sumApp(ref int a,ref int b,ref int c,ref int d) {
            a = b = c = d = 0;
            DateTime? appdate = DateTime.Parse(DateTime.Now.ToShortDateString());
            a =this.DbContext.Query<AppointmentData>().Where(v => v.AppointmentDate== appdate).ToList().Count();
            b = this.DbContext.Query<AppointmentData>().Where(v => v.AppointmentDate == appdate && (v.State==2|| v.State == 1)).ToList().Count();
            c = this.DbContext.Query<AppointmentData>().Where(v => v.AppointmentDate == appdate && v.State ==-2).ToList().Count();
            d = this.DbContext.Query<AppointmentData>().Where(v => v.AppointmentDate == appdate && v.State == -1).ToList().Count();
        }
        public int IsApp(string BusID,string UserID)
        {
            DateTime Stime = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            DateTime Etime = Stime.AddMonths(1).AddDays(-1);
            DateTime NOWTIME = DateTime.Parse((DateTime.Now.ToString("yyyy-MM-dd ") + " 00:00:00"));
            DateTime NOWTIME2 = DateTime.Parse((DateTime.Now.ToString("yyyy-MM-dd ") + " 23:59:59"));
            int ToDayNum= this.DbContext.Query<AppointmentData>().LeftJoin(this.DbContext.Query<MALU_Business>(), (a, b) => a.BusinessID == b.Id).Select((a, b) => new { TransactionID = b.TransactionID, UserID = a.MamberID,State=a.State, CreateTime = a.CreateTime }).Where(a => a.TransactionID == BusID && a.UserID == UserID&&a.State!=-1 && a.CreateTime>= NOWTIME && a.CreateTime <= NOWTIME2).ToList().Count();
            int ToMonthsNum = this.DbContext.Query<AppointmentData>().LeftJoin(this.DbContext.Query<MALU_Business>(), (a, b) => a.BusinessID == b.Id).Select((a, b) => new { TransactionID = b.TransactionID, UserID = a.MamberID, State = a.State, CreateTime=a.CreateTime }).Where(a => a.TransactionID == BusID && a.UserID == UserID && a.State != -1 && a.CreateTime >= Stime && a.CreateTime <= Etime).ToList().Count();
            string Mvalue=this.DbContext.Query<MALU_DefaultSetting>().Where(a => a.KeyName == "每月同一业务预约次数" || a.Id == "8bdf7cba605a4adf8074d70c37930adb").Select(a => a.Value).First();
            string Dvalue = this.DbContext.Query<MALU_DefaultSetting>().Where(a => a.KeyName == "每天同一业务预约次数" || a.Id == "1ef03475df694efd954d78cc0de2a477").Select(a => a.Value).First();
            int Months = 0;
            int DayNum = 0; 
            try {
                Months = Int32.Parse(Mvalue);
                DayNum = Int32.Parse(Dvalue);
                if (DayNum <= ToDayNum) return 1;
                if (Months <= ToMonthsNum) return 2;
                return 0;
            } catch { return 0; }
        }

        public int CancelAppNum(string UserID)
        {
            DateTime Stime = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
            DateTime Etime = Stime.AddMonths(1).AddDays(-1);
            return this.DbContext.Query<AppointmentData>().Where(a => a.MamberID== UserID && a.State== -2&&a.CreateTime>=Stime&&a.CreateTime<=Etime).ToList().Count();
        }
        public bool HasAppointment(AddAppointmentDataInput input) { 
          return this.DbContext.Query<AppointmentData>().Where(a => a.MamberID == input.MamberID && a.State ==0 && a.AppointmentDate==input.AppointmentDate && a.BusinessID==input.BusinessID).ToList().Count()==0;
        }
        public List<SelAppointmentData> GetBusListByUserID(string MID)
        {
            var q = this.DbContext.Query<AppointmentData>().LeftJoin(this.DbContext.Query<MALU_Business>(), (a, b) => a.BusinessID == b.Id).LeftJoin(this.DbContext.Query<PlaceInfo>(), (a, b, p) => b.PlaceId == p.Id)
                .LeftJoin(this.DbContext.Query<PeriodTime>(), (a, b, p, per) => b.PeriodTimeID == per.Id).
                LeftJoin(this.DbContext.Query<TransactionInfo>(), (a, b, p, per, t) => b.TransactionID == t.Id);
            List<SelAppointmentData> view = q.Select((a, b, p, per, t) => new SelAppointmentData
            {
                Id = a.Id,
                TransactionID = b.TransactionID,
                PlaceName = p.PlaceName,
                PlaceId = b.PlaceId,
                TranName = t.TransactionName,
                PeriodTime = per.StratTime + "-" + per.EndTime,
                PeriodTimeID = per.Id,
                CreateTime = a.CreateTime,
                MALU_Code = a.MALU_Code,
                PlaceAdderss = p.Address,
                BusinessID = b.Id,
                AppointmentDate = a.AppointmentDate,
                State = a.State,
                MamberID = a.MamberID,
                BookingNo=a.BookingNo
            }).Where(a => a.MamberID == MID).ToList();
            for (int i = 0; i < view.Count; i++)
            {
                string id = view[i].BusinessID;
                //DateTime dt = view[i].AppointmentDate.Value;
                //if (DateTime.Now.Year == dt.Year && DateTime.Now.Date == dt.Date && DateTime.Now.Month == dt.Month && view[i].State != -1)
                //{
                //    WebService.WebService ws = new WebService.WebService();
                //    string mID = view[i].MamberID;
                //    string IDCard = this.DbContext.Query<MALU_Members>().Where(a => a.Id == mID).Select(a => new { IDCard = a.IdCard }).ToList()[0].IDCard;
                //    if (view[i].State!=2&&ws.SelectAppState(view[i].SerNo, IDCard) > 0) {
                //        view[i].State = 2;
                //        string CodeID = view[i].Id;
                //        this.DbContext.Update<AppointmentData>(a => a.Id == CodeID, a => new AppointmentData()
                //        {
                //            State =2
                //        }); 
                //    }
                //    view[i].MALU_Code = ws.GetTicketCode(view[i].SerNo, IDCard);
                //} 
                DateTime cdt =view[i].CreateTime;
                view[i].LineUpNumber = this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == id && a.State != -1 && a.State != 2 && a.CreateTime < cdt).ToList().Count();
                //string endtime = view[i].PeriodTime.Split('-')[1];
                //DateTime appdate = view[i].AppointmentDate.Value;
                //if (DateTime.Now > appdate.AddHours(Int32.Parse(endtime.Split(':')[0]) - appdate.Hour).AddMinutes(Int32.Parse(endtime.Split(':')[1]) - appdate.Minute)) {
                //    view[i].State = -1;
                //    UpdateStop(view[i].Id);
                //}
            }
            return view;
        }

        public PagedData<SelAppointmentData> GetPageData(Pagination page,string key, string TransactionID, string PlaceId, string PeriodTimeID,string AppDate,int state)
        {
            var q = this.DbContext.Query<AppointmentData>().
                 LeftJoin(DbContext.Query<MALU_Members>(), (a, m) => a.MamberID == m.Id).
                 LeftJoin(DbContext.Query<MALU_Business>(), (a, m, b) => a.BusinessID == b.Id).
                 LeftJoin(DbContext.Query<PlaceInfo>(), (a, m, b, p) => b.PlaceId == p.Id).
                 LeftJoin(DbContext.Query<PeriodTime>(), (a, m, b, p, per) => b.PeriodTimeID == per.Id);
           var qq = q.Select((a, m, b, p, per) => new SelAppointmentData
            {
                Id = a.Id,
                TransactionID = b.TransactionID,
                PlaceName = p.PlaceName,
                PlaceId = b.PlaceId,
                PeriodTime = per.StratTime + "-" + per.EndTime,
                PeriodTimeID = per.Id,
                CreateTime = a.CreateTime,
                MALU_Code = a.MALU_Code,
                PlaceAdderss = p.Address,
                BusinessID = b.Id,
                AppointmentDate = a.AppointmentDate,
                State = a.State,
                MamberID = a.MamberID,
                TranName = "",
                MName = m.Name,
                IdCard=m.IdCard,
                Phone=m.MobilePhone,
                weiKey=m.WeChatKey,
                BookingNo = a.BookingNo

           });
            qq = qq.WhereIfNotNullOrEmpty(TransactionID, a => a.TransactionID == TransactionID).WhereIfNotNullOrEmpty(PeriodTimeID, a => a.PeriodTimeID == PeriodTimeID)
                .WhereIfNotNullOrEmpty(PlaceId, a => a.PlaceId == PlaceId)
                .WhereIfNotNullOrEmpty(key, a => a.MName.Contains(key) || a.Phone.Contains(key))
                .WhereIfNotNullOrEmpty(AppDate, a => a.AppointmentDate == DateTime.Parse(AppDate))
                .WhereIf(state > -6, a => a.State == state);
            PagedData<SelAppointmentData> pagedData =qq.TakePageData(page);
            for (int i = 0; i < pagedData.DataList.Count; i++)
            {
                var tid = pagedData.DataList[i].TransactionID;
                try
                { 
                   pagedData.DataList[i].TranName = this.DbContext.Query<TransactionInfo>().Where(a => a.Id == tid).Select(a => a.TransactionName).First();
                }
                catch{
                    pagedData.DataList[i].TranName = "业务已删除";
                }
            }
            return pagedData;
        }

        public int UpdataState(string CodeID,int State)
        {
            return this.DbContext.Update<AppointmentData>(a => a.Id == CodeID, a => new AppointmentData()
            {
                State = State
            });
        }
    }
}
