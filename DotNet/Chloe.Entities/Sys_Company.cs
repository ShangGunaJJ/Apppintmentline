﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    public class Sys_Company
    {
        public string Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string name { get; set; }
         
        /// <summary>
        /// 地址
        /// </summary>
        public string dz { get; set; }
      
        /// <summary>
        /// 电话
        /// </summary>
        public string dh { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? createDate { get; set; }
     
        /// <summary>
        /// 优先级别
        /// </summary>
        public int priority { get; set; }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string guid { get; set; }

        /// <summary>
        /// 剩余数据提示
        /// </summary>
        public int expirealertpages { get; set; }

        /// <summary>
        /// 公司模式:0 企业,1 会计师事务所
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 敏感字库
        /// </summary>
        public string TryKeyword { get; set; }

        /// <summary>
        /// 父公司ID
        /// </summary>
        public string ParentID { get; set; }

        public DateTime createtime { get; set; }

        public string CreateUserId { get; set; }
    }
}
