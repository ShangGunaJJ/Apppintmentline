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
        string Delete(string id);
        int Update(UpdateAppointmentDataInput input);
        List<SelAppointmentData> GetBusListByUserID(string MID);
        PagedData<SelAppointmentData> GetPageData(Pagination page, string key, string TransactionID, string PlaceId, string PeriodTimeID, string AppDate,int state);
        int IsApp(string BusID, string UserID);
        int UpdateStop(string ID);
        int CancelAppNum(string UserID);
        int UpdataState(string CodeID,int State);
        int UpdateCode(string id, string code);
        void sumApp(ref int a, ref int b, ref int c, ref int d);
        int Eval(string appID);
        bool HasAppointment(AddAppointmentDataInput input);
        }
}
