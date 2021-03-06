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
    public class PeriodTimeService : AdminAppService, IPeriodTime
    {
        public string Delete(string id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<PeriodTime>(a => id.Contains(a.Id));
                if (mValue > 0)
                {
                    detailCount = this.DbContext.Delete<PeriodTime>(a => id.Contains(a.Id));
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public PeriodTime Add(AddPeriodTimeInput input)
        {
            PeriodTime entity = this.CreateEntity<PeriodTime>();

            entity.SeveraWeeks = input.SeveraWeeks;
            entity.StratTime = input.StratTime;
            entity.EndTime = input.EndTime; 
            return this.DbContext.Insert(entity);
        }

        public bool IsOKPeriod(AddPeriodTimeInput p)
        {
           return this.DbContext.Query<PeriodTime>().Where(a => a.SeveraWeeks == p.SeveraWeeks && a.StratTime == p.StratTime && a.EndTime == p.EndTime).Select(a => AggregateFunctions.Count()).First() == 0;
        }

        public List<SimpleModelcs> GetPerSimple() {
            var models = this.DbContext.Query<PeriodTime>().Select(a => new SimpleModelcs() { Id = a.Id, Name = a.StratTime+"-"+a.EndTime }).ToList();
            return models;
        } 
        public int Update(UpdatePeriodTimeInput input)
        {

            if (this.DbContext.Update<PeriodTime>(a => a.Id == input.Id, a => new PeriodTime()
            {
                SeveraWeeks = input.SeveraWeeks,
                StratTime = input.StratTime,
                EndTime = input.EndTime
            }) > 0)
            {
                return 1;
            }
            return 0;
        }

        public PagedData<PeriodTime> GetPageData(Pagination page, string keyword)
        {
            var q = this.DbContext.Query<PeriodTime>();
            //q.Where(a => a.PlaceName.Contains(keyword) || a.Code.Contains(keyword));
            q = q.OrderBy(a => a.CreateTime);
            PagedData<PeriodTime> pagedData = q.TakePageData(page);
            return pagedData;
        }

    }
}
