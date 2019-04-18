using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
    public class AddWorkingDayInput : ValidationModel
    {
        public string Id { get; set; }

        public DateTime? Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int isWorkingDay { get; set; }
    }
}
