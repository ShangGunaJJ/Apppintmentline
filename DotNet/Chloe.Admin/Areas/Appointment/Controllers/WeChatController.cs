using Ace.IdStrategy;
using Chloe.Admin.Common.Tree;
using Chloe.Application.Interfaces;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Admin.Common;
using Chloe.Entities;
using Chloe.Application.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Timers;

namespace Chloe.Admin.Areas
{
    public class WeChatController : WebController
    {
        System.Timers.Timer myTimer = new System.Timers.Timer(10000);
        // GET: WeChat
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public void RefAccess_Token()
        {
            myTimer.Close();
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimeExecuteTask);
            myTimer.Interval = 100000;
            myTimer.Enabled = true;
            myTimer.Start();
            //UpdateDefaultSettingInput de = new UpdateDefaultSettingInput();
            //using (var client = new HttpClient())
            //{
            //    var responseString = client.GetStringAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxb852406f4b6056e7&secret=dd7d281c0563dd34d3bf84d54a541680");

            //    de.KeyType = 1;
            //    de.KeyName = "Acceess_Token";
            //    de.Value = responseString.Result.ToString();
            //    this.CreateService<IDefaultSettingService>().UpdateByKey(de);
            //}
            //return de.Value;
        }
        private void TimeExecuteTask(object sender, ElapsedEventArgs e)
        {
            try
            {
                UpdateDefaultSettingInput de = new UpdateDefaultSettingInput();
                using (var client = new HttpClient())
                {
                    var responseString = client.GetStringAsync("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxb852406f4b6056e7&secret=dd7d281c0563dd34d3bf84d54a541680");
                    de.KeyType = 1;
                    de.KeyName = "Acceess_Token";
                    de.Value = responseString.Result.ToString();
                    this.CreateService<IDefaultSettingService>().UpdateByKey(de);
                }
            }
            catch
            {

            }
        }

        [HttpGet]
        public string RefAccess_Token1()
        {
            try
            {
                UpdateDefaultSettingInput de = new UpdateDefaultSettingInput();
                string retString = HttpGet("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxb852406f4b6056e7&secret=dd7d281c0563dd34d3bf84d54a541680", "");
                de.KeyType = 1;
                de.KeyName = "Acceess_Token";
                de.Value = retString;
                this.CreateService<IDefaultSettingService>().UpdateByKey(de);
                return de.Value;
            }
            catch (Exception e)
            {
                return e.ToString();

            }
        }
        [HttpGet]
        public _Access_Token GetUserInfo(string code)
        {
            string atstring = HttpGet("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + AppConsts.AppID_WeChat + "&secret=SECRET&code=" + code + "&grant_type=authorization_code", "");
            _Access_Token at = JsonConvert.DeserializeObject<_Access_Token>(atstring);
            string UserInfo = HttpGet("https://api.weixin.qq.com/sns/userinfo?access_token=" + at.access_token + "&openid=" + at.openid + "&lang=zh_CN", "");

            UserInfo ui = JsonConvert.DeserializeObject<UserInfo>(UserInfo);

            ui.MenInfo = this.CreateService<IMembersAppService>().SelectByOpenID(ui.openid);
            return at;
        }
        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        public class _Access_Token
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string openid { get; set; }
            public string scope { get; set; }
        }

        public class UserInfo
        {
            public string openid { get; set; }
            public string nickname { get; set; }
            public int sex { get; set; }
            public string city { get; set; }
            public string headimgurl { get; set; }
            public string country { get; set; }
            public string privilege { get; set; }

            public string unionid { get; set; }

            public List<MALU_Members> MenInfo { get; set; }
        }
    }
}