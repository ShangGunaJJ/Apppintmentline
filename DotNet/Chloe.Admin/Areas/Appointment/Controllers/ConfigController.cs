using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chloe.Application.Models.Appointment;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Admin.Common;
using Ace;
using System.Configuration;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    public class ConfigController : WebController
    {
        // GET: Appointment/Config
        public ActionResult Index()
        {
            this.ViewBag.ConfigHTML = this.CreateService<IDefaultSettingService>().GetKeyValueForName("ConfigHTML");
            this.ViewBag.PerList = this.CreateService<IDefaultSettingService>().SelectFor();
            string WeChatUrl = ConfigurationSettings.AppSettings["WeChatUrl"];
            this.ViewBag.PhoneUrl = WeChatUrl+"/WeChatApp/dist/example/#instructions";
            //this.ViewBag.ConfigHTML = this.CreateService<IDefaultSettingService>().GetKeyValueForName("ConfigHTML");
            return View();
        }

        public ActionResult SetV()
        {
            this.ViewBag.PerList = this.CreateService<IDefaultSettingService>().SelectFor();
            return View();
        }
        [HttpPost]
        public ActionResult AddPar(AddDefaultSettingInput ds)
        {
            if (this.CreateService<IDefaultSettingService>().SelectNumforKeyName(ds.KeyName) > 0) {
                return this.SuccessMsg("已经存在重复参数别名！");
            }
            ds.KeyType = 2;
            this.CreateService<IDefaultSettingService>().Add(ds);
            return this.SuccessMsg("成功！");
        }

        [HttpGet]
        public string GetHtml()
        {
            return this.CreateService<IDefaultSettingService>().GetKeyValueForName("ConfigHTML");
        }

        [HttpGet]
        public string DelByKey(string keyName)
        {
            return this.CreateService<IDefaultSettingService>().DeleteByKey(keyName);
        }
        [HttpPost]
        public ActionResult Update(string Key,string Value)
        {
            this.CreateService<IDefaultSettingService>().UpdateByKey(new UpdateDefaultSettingInput() {  KeyName=Key ,Value= ajax_decode(Value,false)});
            return this.SuccessMsg("成功！");
        }

        public static string ajax_decode(string str, bool bsql)
        {
            str = str.Replace("{@bai@}", "%");
            str = str.Replace("{@dan@}", "'");
            str = str.Replace("{@shuang@}", "\"");
            str = str.Replace("{@kong@}", " ");
            str = str.Replace("{@zuojian@}", "<");
            str = str.Replace("{@youjian@}", ">");
            str = str.Replace("{@and@}", "&");
            str = str.Replace("{@tab@}", "\t");
            str = str.Replace("{@jia@}", "+");
            if (bsql) str = str.Replace("'", "''");
            return str;
        }
    }
}