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
    public class AdjustAppDataService: AdminAppService, IAdjustAppDataService
    {
        public string Delete(List<string> id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<AdjustAppointmentData>(a => id.Contains(a.Id));
                if (mValue > 0)
                {
                    detailCount = this.DbContext.Delete<AdjustAppointmentData>(a => id.Contains(a.Id));
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public AdjustAppointmentData Add(AddAdjustAppDataInput input)
        {
            AdjustAppointmentData entity = new AdjustAppointmentData();
            entity.CreateTime = DateTime.Now;
            entity.Id = input.Id;
            entity.NewAppointmentID = input.NewAppointmentID;
            entity.OldAppointmentID = input.OldAppointmentID;
            entity.remark = input.remark;
            entity.CreateUser = input.CreateUser;
            return this.DbContext.Insert(entity);
        }
        public int Update(UpdateAdjustAppDataInput input)
        {

            if (this.DbContext.Update<AdjustAppointmentData>(a => a.Id == input.Id, a => new AdjustAppointmentData()
            {
                CreateTime = DateTime.Now,
                NewAppointmentID=input.NewAppointmentID,
                OldAppointmentID=input.OldAppointmentID,
                remark=input.remark
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
