using Ace;
using Ace.Application;
using Chloe.Entities;
using Chloe.Application.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Interfaces.Appointment
{
    public interface IBusinessService : IAppService
    {
        string Delete(string id);
        MALU_Business Add(AddBusinessInput input);
        int Update(UpdateBusinessInput input);
        int IsAddSer(string PeriodTimeID, string PlaceId, string TransactionID, string id);
        PagedData<MALU_Business> GetPageData(Pagination page, string TransactionID, string PlaceId, string PeriodTimeID);
        List<SelBusinessInput> GetBusListByPlace(string PlaceId, string key);
        void GetSerNo(string tid, ref int SERNO, ref string StartTime, ref string EndTime, ref string tanName);
    }
}
