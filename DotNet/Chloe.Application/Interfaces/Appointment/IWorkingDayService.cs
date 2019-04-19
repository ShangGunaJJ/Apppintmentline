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
    public interface IWorkingDayService : IAppService
    {
        WorkingDay Add(AddWorkingDayInput input);
        int Delete(DateTime date);
        List<WorkingDay> SelectById(DateTime? date);
        List<WorkingDay> Select();
    }
}
