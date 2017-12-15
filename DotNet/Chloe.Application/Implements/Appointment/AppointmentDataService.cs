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
        public string Delete(List<string> id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<AppointmentData>(a => id.Contains(a.Id));
                if (mValue > 0)
                {
                    detailCount = this.DbContext.Delete<AppointmentData>(a => id.Contains(a.Id));
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public AppointmentData Add(AddAppointmentDataInput input)
        {
            AppointmentData entity = new AppointmentData();
            entity.Id = IdHelper.CreateGuid();
            entity.AppointmentDate = input.AppointmentDate;
            entity.BusinessID = input.BusinessID;
            entity.FileID = input.FileID;
            entity.MALU_Code = input.MALU_Code;
            entity.MamberID = input.MamberID;
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

        public int SelectAppCount(string BusID) {
           return this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == BusID).ToList().Count();
        }
        public List<SelAppointmentData> GetBusListByUserID(string MID)
        {
            var q = this.DbContext.Query<AppointmentData>().LeftJoin(this.DbContext.Query<MALU_Business>(), (a, b) => a.BusinessID == b.Id).LeftJoin(this.DbContext.Query<PlaceInfo>(), (a, b, p) => b.PlaceId == p.Id)
                .LeftJoin(this.DbContext.Query<PeriodTime>(), (a, b, p, per) => b.PeriodTimeID == per.Id).
                LeftJoin(this.DbContext.Query<TransactionInfo>(), (a, b, p, per, t) => b.TransactionID == t.Id);
            List<SelAppointmentData> view = q.Select((a, b, p, per, t) => new SelAppointmentData
            {
                Id = b.Id,
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
                MamberID = a.MamberID
            }).Where(a => a.MamberID == MID).ToList();
            for (int i = 0; i < view.Count; i++)
            {
                view[i].LineUpNumber = this.DbContext.Query<AppointmentData>().Where(a => a.BusinessID == view[i].Id && a.State != -1 && a.State != 2 && a.CreateTime < view[i].CreateTime).ToList().Count();
            }
            return view;
        }
    }
}
