using Ace.IdStrategy;
using Chloe.Application.Interfaces.System;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            MALU_Business entity = new MALU_Business();
            entity.CreateTime = DateTime.Now;
            entity.Id = input.Id;
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
    }
}
