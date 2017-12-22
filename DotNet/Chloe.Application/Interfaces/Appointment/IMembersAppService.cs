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
        string Delete(string id);
        MALU_Members Add(AddMembersInput input);
        int Update(AddMembersInput input);
        List<MALU_Members> SelectByOpenID(string OpenID);
        PagedData<MALU_Members> GetPageData(Pagination page, string keyword);
    }
}
