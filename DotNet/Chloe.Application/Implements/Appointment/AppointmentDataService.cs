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
            entity.CreateTime = DateTime.Now;
            entity.Id = input.Id;
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
                CreateTime = DateTime.Now,
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
    }
}
