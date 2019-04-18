using Ace.IdStrategy;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ace;

namespace Chloe.Application.Implements.Appointment
{
    public class EvaluateService : AdminAppService, IEvaluateService
    {
        public string Delete(string id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<Evaluate>(a => id == a.Id);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + "条。";
        }
        public Evaluate Add(Evaluate input)
        {
            Evaluate entity = new Evaluate();  
            entity.Id = IdHelper.CreateGuid();
            entity.CreateTime = DateTime.Now;
            entity.ApponitID = input.ApponitID;
            entity.MenberID = input.MenberID;
            entity.Star = input.Star;
            entity.Evaluation = input.Evaluation;

            return this.DbContext.Insert(entity);
        }

        public List<Evaluate> SelectById(string Id)
        {
            var q = this.DbContext.Query<Evaluate>();
            List<Evaluate> view = q.Where(a => a.Id == Id ).Select((a) => new Evaluate
            {
                Id = a.Id,
                Evaluation=a.Evaluation,
                Star=a.Star,
                ApponitID=a.ApponitID,
                MenberID=a.MenberID,
                CreateTime=a.CreateTime
              
            }).ToList();
           
            return view;
        }

        public PagedData<SelEvaluate> GetPageData(Pagination page, string key, string TransactionID,string AppDate,int Star)
        {
            var q = this.DbContext.Query<Evaluate>().
                 LeftJoin(DbContext.Query<MALU_Members>(), (a, m) => a.MenberID == m.Id).
                 LeftJoin(DbContext.Query<AppointmentData>(), (a, m, b) => a.ApponitID == b.Id).
                 LeftJoin(DbContext.Query<MALU_Business>(), (a, m, b, p) => b.BusinessID == p.Id).
                 LeftJoin(DbContext.Query<TransactionInfo>(), (a, m, b, p,t) =>t.Id==p.TransactionID);

            var qq = q.Select((a, m, b, p,t) => new SelEvaluate
            {
                Id = a.Id,
                TranName=t.TransactionName,
                MName=m.Name,
                Phone=m.MobilePhone,
                IdCard = m.IdCard, 
                CreateTime = a.CreateTime,
                AppointmentDate = b.AppointmentDate,
                Star = a.Star,
                Evaluation=a.Evaluation,
                ApponitID=a.ApponitID,
                TransactionID=t.Id
            });
            qq = qq.WhereIfNotNullOrEmpty(TransactionID, a => a.TransactionID == TransactionID)
                .WhereIfNotNullOrEmpty(key, a => a.MName.Contains(key) || a.Phone.Contains(key))
                .WhereIfNotNullOrEmpty(AppDate, a => a.AppointmentDate == DateTime.Parse(AppDate))
                .WhereIf(Star > 0, a => a.Star == Star);
            PagedData<SelEvaluate> pagedData = qq.TakePageData(page);
            
            return pagedData;
        }
    }
}
