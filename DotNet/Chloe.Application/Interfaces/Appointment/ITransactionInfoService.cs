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
    public interface ITransactionInfoService : IAppService
    {
        string Delete(string id);
        TransactionInfo Add(AddTransactionInfoInput input);
        int Update(UpdateTransactionInfoInput input);
        List<SimpleModelcs> GetPerSimple();
        int IsAddOrUpdate(string Name, string Code);
        PagedData<TransactionInfo> GetPageData(Pagination page, string keyword);
        List<TransactionInfo> GetPerByID(string id);
        TransactionInfo AddData(int ServiceNo, string TransactionName, string Code);
        int SerUpdate(int ServiceNo, string TransactionName, string Code);
        void DeleteDate(List<int> serS);
        int IsAre(int serNo);
    }
}
