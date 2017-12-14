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
    public interface ITransactionInfoService : IAppService
    {
        string Delete(List<string> id);
        TransactionInfo Add(AddTransactionInfoInput input);
        int Update(UpdateTransactionInfoInput input);
        List<SimpleModelcs> GetPerSimple();
    }
}
