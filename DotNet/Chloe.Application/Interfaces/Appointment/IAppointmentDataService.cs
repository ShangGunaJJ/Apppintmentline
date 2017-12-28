using Ace.Application;
using Chloe.Entities;
using Chloe.Application.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ace;

namespace Chloe.Application.Interfaces.Appointment
{
   public interface IAppointmentDataService: IAppService
    {
        AppointmentData Add(AddAppointmentDataInput input);
        string Delete(List<string> id);
        int Update(UpdateAppointmentDataInput input);
        List<SelAppointmentData> GetBusListByUserID(string MID);
        PagedData<SelAppointmentData> GetPageData(Pagination page, string key);
    }
}
