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
    public interface IEvaluateService : IAppService
    {
        string Delete(string id);
        Evaluate Add(Evaluate input);
        List<Evaluate> SelectById(string Id);
        PagedData<SelEvaluate> GetPageData(Pagination page, string key, string TransactionID, string AppDate, int Star);
    }
}
