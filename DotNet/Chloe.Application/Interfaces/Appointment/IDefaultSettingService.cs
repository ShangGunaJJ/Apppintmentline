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
    public interface IDefaultSettingService : IAppService
    {
        string Delete(List<string> id);
        string DeleteByKey(string KeyName);
        MALU_DefaultSetting Add(AddDefaultSettingInput input);
        int Update(UpdateDefaultSettingInput input);
        int UpdateByKey(UpdateDefaultSettingInput input);
        List<MALU_DefaultSetting> SelectFor();
        int SelectNumforKeyName(string Name);
        string GetKeyValueForName(string Name);

    }
}
