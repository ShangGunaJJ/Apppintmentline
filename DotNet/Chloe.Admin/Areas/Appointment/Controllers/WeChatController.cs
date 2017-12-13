using Ace.IdStrategy;
using Chloe.Admin.Common.Tree;
using Chloe.Application.Interfaces;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using Chloe.Admin.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace Chloe.Admin.Areas
{
    public class WeChatController : WebController
    {
        // GET: WeChat
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string RefAccess_Token() {
            UpdateDefaultSettingInput de = new UpdateDefaultSettingInput();
            using (var client = new HttpClient())
            {
                var responseString = client.GetStringAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxb852406f4b6056e7&secret=dd7d281c0563dd34d3bf84d54a541680");
               
                de.KeyType = 1;
                de.KeyName = "Acceess_Token";
                de.Value = responseString.Result.ToString();
                this.CreateService<IDefaultSettingService>().UpdateByKey(de);
            }
            return de.Value;
        }
    }
}