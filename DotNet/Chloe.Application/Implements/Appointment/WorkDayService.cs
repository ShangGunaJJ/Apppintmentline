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
    public class WorkDayService: AdminAppService, IWorkingDayService
    {
        public WorkingDay Add(AddWorkingDayInput input)
        {
            WorkingDay entity = new WorkingDay();
            entity.Id = IdHelper.CreateGuid();
            entity.Date = input.Date;
            entity.isWorkingDay = input.isWorkingDay;  
            return this.DbContext.Insert(entity);
        }

        public int Delete(DateTime date)
        {  
            return this.DbContext.Delete<WorkingDay>(a => date == a.Date);
        }

        public List<WorkingDay> SelectById(DateTime date)
        {
            var q = this.DbContext.Query<WorkingDay>();
            List<WorkingDay> view = q.Where(a => a.Date == date).Select((a) => new WorkingDay
            {
                Id = a.Id,
                Date=a.Date,
                isWorkingDay=a.isWorkingDay

            }).ToList();
            return view;
        }

        public List<WorkingDay> Select() {
            var q = this.DbContext.Query<WorkingDay>();
            List<WorkingDay> view = q.Select((a) => new WorkingDay
            {
                Id = a.Id,
                Date = a.Date,
                isWorkingDay = a.isWorkingDay

            }).ToList();
            return view; 
        }
    }
}
