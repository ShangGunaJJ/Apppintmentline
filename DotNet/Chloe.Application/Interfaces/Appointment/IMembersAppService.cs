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
    public interface  IMembersAppService: IAppService
    {
        string Delete(List<string> id);
        MALU_Members Add(AddMembersInput input);
        int Update(AddMembersInput input);
        List<MALU_Members> SelectByOpenID(string OpenID);
    }
}
