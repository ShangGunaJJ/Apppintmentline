using Ace.IdStrategy;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;using Ace;

namespace Chloe.Application.Implements.Appointment
{
    public class BusinessService: AdminAppService,IBusinessService
    {
        public string Delete(string id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<MALU_Business>(a => id.Contains(a.Id));
                if (mValue > 0)
                {
                    detailCount = this.DbContext.Delete<MALU_Business>(a => id.Contains(a.Id));
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public int IsAddSer(string PeriodTimeID, string PlaceId, string TransactionID, string id)
        {
            if (this.DbContext.Query<MALU_Business>().Where(a => a.PeriodTimeID == PeriodTimeID && a.TransactionID == TransactionID && a.PlaceId == PlaceId).WhereIfNotNullOrEmpty(id, a => a.Id != id).ToList().Count() > 0)
                return 1;

            var Peiview = this.DbContext.Query<PeriodTime>().Where(a => a.Id == PeriodTimeID).Select(a => new PeriodTime { StratTime = a.StratTime, EndTime = a.EndTime, SeveraWeeks = a.SeveraWeeks }).ToList();
            DateTime startTime = DateTime.Parse("2018-01-01 " + Peiview[0].StratTime);
            DateTime endTime = DateTime.Parse("2018-01-01 " + Peiview[0].EndTime);
            int weeks = Peiview[0].SeveraWeeks;
            var view = this.DbContext.Query<MALU_Business>().LeftJoin(this.DbContext.Query<PeriodTime>(), (a, b) => a.PeriodTimeID == b.Id).Select((a, b) => new { Id=a.Id,StartTime=b.StratTime,EndTime=b.EndTime, PlaceId=a.PlaceId, TransactionID=a.TransactionID, SeveraWeeks = b.SeveraWeeks })
                .Where(a => a.TransactionID == TransactionID && a.PlaceId == PlaceId&& a.SeveraWeeks== weeks).WhereIfNotNullOrEmpty(id, a => a.Id != id).ToList();
            if (view.Count > 0)
            {
                for (int i = 0; i < view.Count; i++)
                {
                    DateTime _startTime = DateTime.Parse("2018-01-01 " + view[i].StartTime);
                    DateTime _endTime = DateTime.Parse("2018-01-01 " + view[i].EndTime);
                    if ((startTime >= _startTime && startTime < _endTime)||(_startTime >= startTime && _startTime < endTime))
                        return 2;
                    if (endTime >= _startTime && endTime < _endTime)
                        return 3;
                }
            }
            return 0;
        }
        public MALU_Business Add(AddBusinessInput input)
        {
            MALU_Business entity = this.CreateEntity<MALU_Business>(); 
            entity.AppointmentNum = input.AppointmentNum;
            entity.IsWeekEnd = (IsWeekEnd)input.IsWeekEnd;
            entity.PeriodTimeID = input.PeriodTimeID;
            entity.PlaceId = input.PlaceId;
            entity.TransactionID = input.TransactionID;
            return this.DbContext.Insert(entity);
        }
        public int Update(UpdateBusinessInput input)
        {

            if (this.DbContext.Update<MALU_Business>(a => a.Id == input.Id, a => new MALU_Business()
            {
                UpdateTime = DateTime.Now,
                AppointmentNum = input.AppointmentNum,
                IsWeekEnd = (IsWeekEnd)input.IsWeekEnd,
                PeriodTimeID = input.PeriodTimeID,
                PlaceId = input.PlaceId,
                TransactionID = input.TransactionID
            }) > 0)
            {
                return 1;
            }
            return 0;
        }

        public PagedData<MALU_Business> GetPageData(Pagination page,string TransactionID,string PlaceId,string PeriodTimeID)
        {
            var q = this.DbContext.Query<MALU_Business>();
            if (TransactionID != "")
                if (PlaceId != "")
                {
                    if (PeriodTimeID != "")
                        q = q.Where(a => a.TransactionID == TransactionID && a.PeriodTimeID == PeriodTimeID && a.PlaceId == PlaceId);
                    else
                        q = q.Where(a => a.TransactionID == TransactionID && a.PlaceId == PlaceId);
                }
                else
                {
                    if (PeriodTimeID != "")
                        q = q.Where(a => a.TransactionID == TransactionID && a.PeriodTimeID == PeriodTimeID);
                    else
                        q = q.Where(a => a.TransactionID == TransactionID);
                }
            else
            {
                if (PlaceId != "")
                {
                    if (PeriodTimeID != "")
                        q = q.Where(a => a.PeriodTimeID == PeriodTimeID && a.PlaceId == PlaceId);
                    else
                        q = q.Where(a => a.PlaceId == PlaceId);
                }
                else
                {
                    if (PeriodTimeID != "")
                        q = q.Where(a => a.PeriodTimeID == PeriodTimeID);
                }
            }

            q = q.OrderBy(a => a.CreateTime);
            PagedData<MALU_Business> pagedData = q.TakePageData(page);
            return pagedData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlaceId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<SelBusinessInput> GetBusListByPlace(string PlaceId,string key)
         {
           
            var q = this.DbContext.Query<MALU_Business>().LeftJoin(this.DbContext.Query<PlaceInfo>(), (b, p) => b.PlaceId == p.Id)
                .LeftJoin(this.DbContext.Query<PeriodTime>(), (b, p,per) => b.PeriodTimeID == per.Id).
                LeftJoin(this.DbContext.Query<TransactionInfo>(), (b, p, per,t) => b.TransactionID == t.Id);
            List<SelBusinessInput> view = q.Select((b, p, per, t) => new SelBusinessInput
            {
                Id=b.Id,
                AppointmentNum=b.AppointmentNum,
                TransactionID=b.TransactionID,
                PlaceName=p.PlaceName,
                PlaceAdderss=p.Address,
                PlaceId =b.PlaceId,
                TranName=t.TransactionName,
                PeriodTimeId=per.Id,
                PeriodTime= per.StratTime + "-" + per.EndTime,
                Weeks=per.SeveraWeeks,
                SerNo=t.ServiceNo
            }).WhereIfNotNullOrEmpty(PlaceId, a=>a.PlaceId==PlaceId).WhereIfNotNullOrEmpty(key,a=>a.TranName.Contains(key)).ToList();
            for (int i = 0; i < view.Count; i++)
            {
                string BiD = view[i].Id;
                DateTime? now = DateTime.Now;
                view[i].LineUpNumber=this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == BiD && a.State!=-1&&a.AppointmentDate>= now).Select(a => AggregateFunctions.Count()).First();
                //try
                //{  
                //    view[i].NowAppCode = this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == BiD && a.State == 1).Select(a => a.MALU_Code).First();
                //}
                //catch {
                //    view[i].NowAppCode = "";
                //}
             
            }
            return view;
        }

        public void GetSerNo(string tid,ref int SERNO,ref string StartTime,ref string EndTime,ref string tanName)
        {
            var view= this.DbContext.Query<MALU_Business>().LeftJoin(this.DbContext.Query<PeriodTime>(),(a,b)=>a.PeriodTimeID==b.Id).LeftJoin(this.DbContext.Query<TransactionInfo>(), (a, b,t) => a.TransactionID == t.Id).Select((a,b,t)=>new { SERNO =t.ServiceNo,id=a.Id,StartTime=b.StratTime,EndTime=b.EndTime,Name=t.TransactionName}).Where(a=>a.id== tid).ToList()[0];
            SERNO = view.SERNO;
            StartTime = view.StartTime;
            EndTime = view.EndTime;
            tanName = view.Name;
        }
    }
}
