using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
  public class PeriodTime
    {
        public string Id { get; set; }
        /// <summary>
        /// 星期几
        /// </summary>
        public int SeveraWeeks { get; set; }

        public DateTime? StratTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
