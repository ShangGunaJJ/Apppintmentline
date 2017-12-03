using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    /// <summary>
    /// 审批预约表
    /// </summary>
    public class ApprovalAppData
    {
        public string Id { get; set; }

        /// <summary>
        /// 预约业务数据ID
        /// </summary>
        public string AppDataID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string AppUser { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? AppTime { get; set; }
      
        public DateTime? CreateTime { get; set; }
       
    }

    public class inv_main_sea:ApprovalAppData {

    }
}
