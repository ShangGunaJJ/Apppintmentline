using Ace;
using Ace.IdStrategy;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.Appointment
{
    public class TransactionInfoService : AdminAppService,ITransactionInfoService
    {
        public string Delete(string id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<TransactionInfo>(a => id.Contains(a.Id));
                if (mValue > 0)
                {
                    detailCount = this.DbContext.Delete<TransactionInfo>(a => id.Contains(a.Id));
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public int IsAddOrUpdate(string Name,string Code) {
            return this.DbContext.Query<TransactionInfo>().Where(a => a.Code == Code).ToList().Count();
        }
        public TransactionInfo Add(AddTransactionInfoInput input)
        {
            TransactionInfo entity = this.CreateEntity<TransactionInfo>();
            entity.TransactionName = input.TransactionName;
            entity.Code = input.Code;
            entity.Describe = input.Describe;
            entity.IsUploadFile = input.IsUploadFile;
            entity.IsApproval = input.IsApproval;
            entity.IsAutoCode = input.IsAutoCode;
            entity.ServiceNo = input.ServiceNo;
            return this.DbContext.Insert(entity);
        }
        public TransactionInfo AddData(int ServiceNo, string TransactionName, string Code)
        {
            TransactionInfo entity = this.CreateEntity<TransactionInfo>();
            entity.TransactionName = TransactionName;
            entity.Code = Code;
            entity.Describe ="";
            entity.IsUploadFile = 0;
            entity.IsApproval = 0;
            entity.IsAutoCode = 0;
            entity.ServiceNo = ServiceNo;
            return this.DbContext.Insert(entity);
        }
        public int SerUpdate(int ServiceNo,string TransactionName,string Code)
        {

            if (this.DbContext.Update<TransactionInfo>(a => a.ServiceNo == ServiceNo, a => new TransactionInfo()
            {
                TransactionName = TransactionName,
                Code = Code
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
        public int IsAre(int serNo)
        {
            return this.DbContext.Query<TransactionInfo>().Where(a => a.ServiceNo == serNo).ToList().Count();
        }
        public void DeleteDate(List<int> serS) {
            this.DbContext.Delete<TransactionInfo>(a => !serS.Contains(a.ServiceNo));
        }
        public int Update(UpdateTransactionInfoInput input)
        {

            if (this.DbContext.Update<TransactionInfo>(a => a.Id == input.Id, a => new TransactionInfo()
            {
                Describe = input.Describe,
                CreateUser = input.CreateUser,
                IsUploadFile = input.IsUploadFile,
                IsApproval = input.IsApproval,
                IsAutoCode = input.IsAutoCode,
                UpdateTime = DateTime.Now
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
        public List<SimpleModelcs> GetPerSimple()
        {
            var models = this.DbContext.Query<TransactionInfo>().Select(a => new SimpleModelcs() { Id = a.Id, Name = a.TransactionName }).ToList();
            return models;
        }
        public PagedData<TransactionInfo> GetPageData(Pagination page, string keyword)
        {
            var q = this.DbContext.Query<TransactionInfo>();
            q = q.WhereIfNotNullOrEmpty(keyword,a => a.TransactionName==keyword || a.Code==keyword); 
            q = q.OrderBy(a => a.CreateTime);
            PagedData<TransactionInfo> pagedData = q.TakePageData(page);
            return pagedData;
        }
        public List<TransactionInfo> GetPerByID(string id)
        {
            var models = this.DbContext.Query<TransactionInfo>().Where(a=>a.Id==id).ToList();
            return models;
        }
    }
}
