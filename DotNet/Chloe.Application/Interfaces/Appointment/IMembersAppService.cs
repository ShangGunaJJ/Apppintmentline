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
    public interface  IMembersAppService: IAppService
    {
        int Delete(string Id, int _state);
        MALU_Members Add(AddMembersInput input);
        int Update(AddMembersInput input);
        void SelectByOpenID(string OpenID, ref string Id, ref string Name, ref string IdCard, ref string phone, ref int state);
        PagedData<MALU_Members> GetPageData(Pagination page, string keyword, int? state);
        int IsReg(string IDCARD);
        MALU_Members GetInfoByID(string ID);
        string GetIDCard(string mID);
    }
}
