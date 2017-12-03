using Ace.Application;
using Chloe.Entities;
using Chloe.Application.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chloe.Application.Interfaces.System
{
    public interface IAdjustAppDataService : IAppService
    {
        string Delete(List<string> id);
        AdjustAppointmentData Add(AddAdjustAppDataInput input);
        int Update(UpdateAdjustAppDataInput input);
    }
}
