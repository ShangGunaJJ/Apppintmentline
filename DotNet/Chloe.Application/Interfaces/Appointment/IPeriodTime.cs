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
    public interface  IPeriodTime: IAppService
    {
        string Delete(string id);
        PeriodTime Add(AddPeriodTimeInput input);
        int Update(UpdatePeriodTimeInput input);
        List<SimpleModelcs> GetPerSimple();
        PagedData<PeriodTime> GetPageData(Pagination page, string keyword);
        bool IsOKPeriod(AddPeriodTimeInput p);
    }
}
