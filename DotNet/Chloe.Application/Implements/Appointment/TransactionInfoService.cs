﻿using Ace;
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
        public TransactionInfo Add(AddTransactionInfoInput input)
        {
            TransactionInfo entity = this.CreateEntity<TransactionInfo>();
            entity.TransactionName = input.TransactionName;
            entity.Code = input.Code;
            entity.Describe = input.Describe;
            entity.IsUploadFile = input.IsUploadFile;
            entity.IsApproval = input.IsApproval;
            entity.IsAutoCode = input.IsAutoCode;
            return this.DbContext.Insert(entity);
        }
        public int Update(UpdateTransactionInfoInput input)
        {

            if (this.DbContext.Update<TransactionInfo>(a => a.Id == input.Id, a => new TransactionInfo()
            {
                TransactionName = input.TransactionName,
                Code = input.Code,
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
            q.Where(a => a.TransactionName==keyword || a.Code==keyword); 
            q = q.OrderBy(a => a.CreateTime);
            PagedData<TransactionInfo> pagedData = q.TakePageData(page);
            return pagedData;
        }
    }
}
