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
using Ace.VerifyCode;

namespace Chloe.Admin.Areas
{
    public class WeChatController : WebController
    {
        System.Timers.Timer myTimer = new System.Timers.Timer(10000);
        const string VerifyCodeKey = "session_verifycode";
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
        public void RefUrl() {
            Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorizeappid="+  AppConsts.AppID_WeChat  + "&redirect_uri="+Server.UrlEncode("http://47.93.230.40:8000/WeChatapp/dist/example/")+"&response_type=code&scope=SCOPE&state=STATE#wechat_redirect");
        }

        [HttpGet]
        public ContentResult GetUserInfo(string code)
        {
            string atstring = HttpGet("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + AppConsts.AppID_WeChat + "&secret=SECRET&code=" + code + "&grant_type=authorization_code", "");
            _Access_Token at = JsonConvert.DeserializeObject<_Access_Token>(atstring);
            string UserInfo = HttpGet("https://api.weixin.qq.com/sns/userinfo?access_token=" + at.access_token + "&openid=" + at.openid + "&lang=zh_CN", "");

            UserInfo ui = JsonConvert.DeserializeObject<UserInfo>(UserInfo);

            ui.MenInfo = this.CreateService<IMembersAppService>().SelectByOpenID(ui.openid);
            return this.JsonContent(ui);
        }
        [HttpGet]
        public ContentResult GetBusListByPlace(string PlaceID) {
           List<SelBusinessInput>  sbl= this.CreateService<IBusinessService>().GetBusListByPlace(PlaceID);
            return this.JsonContent(sbl);
        }

        [HttpGet]
        public ContentResult GetMyBusList(string UserID)
        {
            List<SelAppointmentData> sbl = this.CreateService<IAppointmentDataService>().GetBusListByUserID(UserID);
            return this.JsonContent(sbl);
        }

        [HttpPost]
        //return 1 是成功，2是验证码错误，3 失败
        public int AddUser(AddMembersInput mms)
        {
            string code = WebHelper.GetSession<string>(VerifyCodeKey);
            WebHelper.RemoveSession(VerifyCodeKey);
            if (code.IsNullOrEmpty() || code.ToLower() != mms.VCode.ToLower())
            {
                return 2;
            }
            return this.CreateService<IMembersAppService>().Add(mms).Id != "" ? 3 : 1;
        }

        [HttpGet]
        public ActionResult VerifyCode()
        {
            string verifyCode = VerifyCodeGenerator.GenVerifyCode();
            byte[] bytes = VerifyCodeHelper.GetVerifyCodeByteArray(verifyCode);

            WebHelper.SetSession(VerifyCodeKey, verifyCode);

            return this.File(bytes, @"image/Gif");
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