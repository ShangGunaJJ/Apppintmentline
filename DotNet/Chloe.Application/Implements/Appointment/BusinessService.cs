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
        public string Delete(List<string> id)
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
        public MALU_Business Add(AddBusinessInput input)
        {
            MALU_Business entity = this.CreateEntity<MALU_Business>();
            entity.CreateUser = input.CreateUser;
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

        public PagedData<MALU_Business> GetPageData(Pagination page)
        {
            var q = this.DbContext.Query<MALU_Business>();
            q = q.OrderBy(a => a.CreateTime);
            PagedData<MALU_Business> pagedData = q.TakePageData(page);
            return pagedData;
        }

        public List<SelBusinessInput> GetBusListByPlace(string PlaceId)
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
                PlaceId=b.PlaceId,
                TranName=t.TransactionName
            }).Where(a=>a.PlaceId==PlaceId).ToList();
            for (int i = 0; i < view.Count; i++)
            {
                view[i].LineUpNumber=this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == view[i].Id&&a.State!=-1).ToList().Count();
                view[i].NowAppCode=this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == view[i].Id && a.State == 1).Select(a => a.MALU_Code).First();
            }
            return view;
        }
    }
}
