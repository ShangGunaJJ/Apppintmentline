using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    public class WorkingDay
    {
        public string Id { get; set; }

        public DateTime? Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int isWorkingDay { get; set; }
    }
}
