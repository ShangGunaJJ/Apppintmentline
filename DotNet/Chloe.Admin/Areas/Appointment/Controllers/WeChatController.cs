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
using System.Net.Http.Headers;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Configuration;
using Chloe.Entities.Enums;
using System.Web.Security;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Chloe.Admin.Areas
{
    public class WeChatController : WebController
    {
        System.Timers.Timer myTimer = new System.Timers.Timer(10000);
        const string VerifyCodeKey = "session_verifycode";
        const string SMSCodeKey = "SMSCodeKey";
        string AppSecret_WeChat = ConfigurationSettings.AppSettings["WeChatAppSecret"];
        string AppID_WeChat = ConfigurationSettings.AppSettings["WeChatAppId"];
        string WeChatUrl = ConfigurationSettings.AppSettings["WeChatUrl"];
        string BackUrl = ConfigurationSettings.AppSettings["BackUrl"];
        private string Token = ConfigurationManager.AppSettings["Token"];
        string pHostIP = ConfigurationManager.AppSettings["pHostIP"];
        public ActionResult Index()
        {
            return View();
        }

        #region 服务器
        [HttpGet] 
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
           
            this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "", true, "服务器验证:signature" + signature + ";timestamp:" + timestamp + ";nonce:" + nonce + ";echostr:" + echostr);
            if (Check(signature, timestamp, nonce, Token))
            {
                return Content(echostr);//返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + signature + "," + GetSignature(timestamp, nonce, Token) + "。如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }

        }
        [HttpPost]
        [ActionName("Index")]
        public void Get()
        {
            if (HttpContext.Request.HttpMethod.ToUpper() == "POST")
            {

                string postString;
                using (Stream stream = HttpContext.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                    this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "", true, postString);
                    EventHandle(postString);
                }

            }
        }

        /// <summary>
        /// 微信菜单点击事件
        /// </summary>
        /// <param name="postStr">接受消息</param>
        /// <returns></returns>
        private void EventHandle(string postStr)
        {
            string responseContent = "";
            try
            {
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                xmldoc.LoadXml(postStr);
                this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "", true, "微信菜单点击: "+ "------------------" + postStr);
                XmlNode Event = xmldoc.SelectSingleNode("/xml/Event");
                XmlNode EventKey = xmldoc.SelectSingleNode("/xml/EventKey");
                XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
                XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");

                if (Event != null)
                {
                    if (EventKey.InnerText.Equals("V1001_ORDER"))//EventKey.InnerText.Equals("V1001_ORDER") 就是创建菜单时，click按钮的唯一Key
                    {
                        responseContent = string.Format(Message_Text,
                             FromUserName.InnerText,
                             ToUserName.InnerText,
                             DateTime.Now.Ticks,
                             "您好，小萌为您服务，请回复十四位订单号，查询订单详情。");//生成文本消息
                    }
                    string Content = "";
                    responseContent = ReceivedText(FromUserName.InnerText, ToUserName.InnerText, Content);

                }
                this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "", true, "微信菜单点击:signature" + responseContent + "------------------" + postStr);
                HttpContext.Response.Write(responseContent);//返回给微信
            }
            catch (Exception ex)
            {
                this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "", true, "微信菜单点击事件" + "||" + ex.ToString());
            }
        }
        public string ReceivedText(string FromUserName, string ToUserName, string Content)
        {
            DateTime DateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            string textpl = string.Empty;
            textpl = "<xml>" +
                     "<ToUserName><![CDATA[" + FromUserName + "]]></ToUserName>" +
                     "<FromUserName><![CDATA[" + ToUserName + "]]></FromUserName>" +
                     "<CreateTime>" + Convert.ToInt32((DateTime.Now - DateStart).TotalSeconds)+ "</CreateTime>" +
                     "<MsgType><![CDATA[text]]></MsgType>" +
                     "<Content><![CDATA[" + Content + "]]></Content>" +
                     "<FuncFlag>0</FuncFlag>" +
                     "</xml>";

            return textpl;
        }
        /// <summary>
        /// 普通文本消息
        /// </summary>
        public static string Message_Text
        {
            get
            {
                return @"<xml>                             
                             <ToUserName><![CDATA[{0}]]></ToUserName>                             
                             <FromUserName><![CDATA[{1}]]></FromUserName>
                             <CreateTime>{2}</CreateTime>
                             <MsgType><![CDATA[text]]></MsgType>
                             <Content><![CDATA[{3}]]></Content>
                             </xml>";
            }
        }

        /// <summary>
        /// 检查签名是否正确
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Check(string signature, string timestamp, string nonce, string token = null)
        {
            return signature == GetSignature(timestamp, nonce, token);
        }

        /// <summary>
        /// 返回正确的签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public  string GetSignature(string timestamp, string nonce, string token = null)
        {
            token = token ?? Token;
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            //var enText = FormsAuthentication.HashPasswordForStoringInConfigFile(arrString, "SHA1");//使用System.Web.Security程序集
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }

            return enText.ToString();
        }
        #endregion




        [HttpGet]
        public void RefAccess_Token()
        {
            TimeExecuteTask(null,null);
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimeExecuteTask);
            myTimer.Interval = 7000000;
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
        public void TimeExecuteTask(object sender, ElapsedEventArgs e)
        {
            try
            {
                UpdateDefaultSettingInput de = new UpdateDefaultSettingInput();
                string retString = HttpGet(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppID_WeChat, AppSecret_WeChat),"");
                de.KeyType = 1;
                de.KeyName = "Acceess_Token";
                de.Value = retString;
                this.CreateService<IDefaultSettingService>().UpdateByKey(de);
            }
            catch
            {

            }
        }
        [HttpGet]
        public void OpenAccess(string PlaceID) {
            //string postString;
            //using (Stream stream = HttpContext.Request.InputStream)
            //{
            //    Byte[] postBytes = new Byte[stream.Length];
            //    stream.Read(postBytes, 0, (Int32)stream.Length);
            //    postString = Encoding.UTF8.GetString(postBytes);
            //}
            //this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "", true, postString);
            if (Session["openid"] == null)
            {

                //认证第一步：重定向跳转至认证网址
                string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&&response_type=code&scope=snsapi_userinfo&m=oauth2#wechat_redirect",AppID_WeChat, Server.UrlEncode(WeChatUrl+"/IsReg.html?PlaceID=" + PlaceID));
                Response.Redirect(url);
            }
            //判断session存在
            else
            {
                //跳转到前端页面.aspx
                Response.Redirect(WeChatUrl+"/IsReg.html?PlaceID=" + PlaceID);
            }
        }
        [HttpGet]
        public void OpenAccessTakeNo(string PlaceID)
        {
            if (Session["openid"] == null) {
                //认证第一步：重定向跳转至认证网址
                string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&&response_type=code&scope=snsapi_userinfo&m=oauth2#wechat_redirect", AppID_WeChat, Server.UrlEncode(WeChatUrl + "/GetCode.html?PlaceID=" + PlaceID));
                Response.Redirect(url);
            }
            //判断session存在
            else
            {
                //跳转到前端页面.aspx
                Response.Redirect(WeChatUrl + "/GetCode.html?PlaceID=" + PlaceID);
            }
        }

        [HttpGet]
        public string SMSSend(string phone)
        {
            SMS.WebService SW = new SMS.WebService();
            Random r = new Random();
            int vcode= r.Next(1000, 10000);
            WebHelper.SetSession(SMSCodeKey, vcode.ToString());
            return SW.sendsms(phone, "您注册深圳中智户籍约为网上预约的验证码为：" + vcode, "7550D0F39DA9350B26433CA6FA8EF24F");
        }

        [HttpGet]
        public string RefAccess_Token1()
        {
            try
            {
                UpdateDefaultSettingInput de = new UpdateDefaultSettingInput();
                string retString = HttpGet("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx704397e6f70bb8d5&secret=865867c73ac14944ec4eba64d7ebdf52", "");
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

        #region 订阅号
        [HttpGet]
        public void OpenAccess2(string PlaceID,string SerUrl) {
            if (Session["openid"] == null)
            {
                //认证第一步：重定向跳转至认证网址
                string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&&response_type=code&scope=snsapi_userinfo&m=oauth2#wechat_redirect", AppID_WeChat, Server.UrlEncode(WeChatUrl + "/WeChat/GetUserInfo2?PlaceID=" + PlaceID+"&SerUrl="+ SerUrl));
                Response.Redirect(url);
            }
            //判断session存在
            else
            {
                //跳转到前端页面.aspx
                Response.Redirect(WeChatUrl + "/IsReg.html?PlaceID=" + PlaceID);
            }
        }

        [HttpGet]
        public void GetUserInfo2(string code,string SerUrl,string PlaceID)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", AppID_WeChat, AppSecret_WeChat, code);
            string atstring = HttpClientHelper.GetResponse(url);
            _Access_Token at = JsonConvert.DeserializeObject<_Access_Token>(atstring);

            url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", at.access_token, at.openid);
            string UserInfo = HttpClientHelper.GetResponse(url);
            this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "", true, at.openid);
            this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "获取用户信息", true, UserInfo);
            JObject JO = (JObject)JsonConvert.DeserializeObject(UserInfo);
            if (at.openid != "")
                Response.Redirect(SerUrl + "/GoOut.html?openid=" + at.openid+ "&PlaceID="+ PlaceID+"&Img="+ JO["headimgurl"].ToString());
        }
        [HttpGet]
        public ContentResult GetUserInfo3(string openid)
        {
            //string access_token=this.CreateService<IDefaultSettingService>().GetKeyValueForName("Acceess_Token");
            //JObject JO= (JObject)JsonConvert.DeserializeObject(access_token);
            //string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", JO["access_token"].ToString(), openid);
            //this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "当前Acceess_Token", true, JO["access_token"].ToString());
            //string UserInfo = HttpClientHelper.GetResponse(url);

            UserInfo ui = new UserInfo();
            string Id = "";
            string Name = "";
            string IdCard = "";
            string MobilePhone = "";
            int state = 1;
            this.CreateService<IMembersAppService>().SelectByOpenID(openid, ref Id, ref Name, ref IdCard, ref MobilePhone, ref state);
            ui.Id = Id;
            ui.Name = Name;
            ui.IdCard = IdCard;
            ui.MobilePhone = MobilePhone;
            //ui.ReValue = UserInfo;
            ui.State = state;
            ui.openid =openid;
            ui.CancelNum = (Id != "" ? this.CreateService<IAppointmentDataService>().CancelAppNum(Id) : 0);
            return this.JsonContent(ui);
        }
        #endregion
        [HttpGet]
        public ContentResult GetUserInfo(string code)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", AppID_WeChat, AppSecret_WeChat, code);
            string atstring = HttpClientHelper.GetResponse(url);
            _Access_Token at = JsonConvert.DeserializeObject<_Access_Token>(atstring);
           
            url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", at.access_token, at.openid);
            string UserInfo = HttpClientHelper.GetResponse(url);
            this.CreateService<ISysLogAppService>().LogAsync("", "", "", LogType.Other, "", true, at.openid);
            Session["openid"] = at.openid;
            UserInfo ui = new UserInfo();
            string Id = "";
            string Name = "";
            string IdCard = "";
            string MobilePhone = "";
            int state = 1;
            this.CreateService<IMembersAppService>().SelectByOpenID(at.openid,ref Id,ref Name,ref IdCard,ref  MobilePhone,ref state);
            ui.Id = Id;
            ui.Name = Name;
            ui.IdCard = IdCard;
            ui.MobilePhone = MobilePhone;
            ui.ReValue = UserInfo;
            ui.State = state;
            ui.openid = at.openid;
            ui.CancelNum = (Id != "" ? this.CreateService<IAppointmentDataService>().CancelAppNum(Id) : 0);
            return this.JsonContent(ui);
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        [HttpGet]
        public int SetStop(string AppID) {
           return this.CreateService<IAppointmentDataService>().UpdateStop(AppID);
        }

        /// <summary>
        /// 通过地点获取业务项
        /// </summary>
        /// <param name="PlaceID"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public ContentResult GetBusListByPlace(string PlaceID,string key) {
           List<SelBusinessInput>  sbl= this.CreateService<IBusinessService>().GetBusListByPlace(PlaceID,key);
            string lis = "";
            foreach (var view in sbl)
            {
                if (lis.IndexOf(view.SerNo.ToString()) == -1){
                    lis += view.SerNo +".";
                    WebService.WebService ws = new WebService.WebService();
                    view.NowAppCode = ws.GetListTicketCode(view.SerNo);
                } 
            } 
            return this.JsonContent(sbl);
        }
        /// <summary>
        /// 获取自己业务列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet]
        public ContentResult GetMyBusList(string UserID)
        {
            var IAppService = this.CreateService<IAppointmentDataService>();
            List<SelAppointmentData> sbl = IAppService.GetBusListByUserID(UserID);
            WebService.WebService ws = new WebService.WebService();
            foreach (var view in sbl)
            {
                string id = view.Id;
                DateTime day = view.AppointmentDate.Value;
                string pertime = view.PeriodTime;
                if (pertime!=""&& pertime!=null)
                {
                    try
                    {
                        DateTime dt = day.AddHours(Int32.Parse(pertime.Split('-')[0].Split(':')[0])).AddMinutes(Int32.Parse(pertime.Split('-')[0].Split(':')[1]));
                        DateTime dt2 = day.AddHours(Int32.Parse(pertime.Split('-')[1].Split(':')[0])).AddMinutes(Int32.Parse(pertime.Split('-')[1].Split(':')[1]));
                        if (view.State == -2)
                            view.State = -1;
                        if (view.State != 2 && view.State != -1 && view.State != -2)
                        {
                            int IsValid = ws.IsValid(view.BookingNo);
                            if (IsValid > 0)
                            {
                                string mID = view.MamberID;
                                string CodeID = view.Id;
                                string IDCard = this.CreateService<IMembersAppService>().GetIDCard(mID);
                                if (DateTime.Now >= dt && DateTime.Now <= dt2)
                                {
                                    view.State = 1;
                                    view.MALU_Code = ws.GetTicketCode(view.SerNo, IDCard);
                                    IAppService.UpdateCode(CodeID, view.MALU_Code);
                                }
                                else if (DateTime.Now > dt2)
                                {
                                    view.State = 2;
                                    if (string.IsNullOrEmpty(view.MALU_Code)) view.MALU_Code = "";
                                    IAppService.UpdataState(id, 2);
                                }
                            }
                            else if (DateTime.Now > dt2)
                            {
                                view.State = -1;
                                view.MALU_Code = "";
                                IAppService.UpdataState(id, -2);
                            }
                            else if (DateTime.Now < dt)
                            {
                                view.State = 0;
                                view.MALU_Code = "";
                                IAppService.UpdataState(id, 0);
                            }
                            else if (DateTime.Now >= dt && DateTime.Now <= dt2)
                            {
                                view.State = 1;
                                view.MALU_Code = "";
                                IAppService.UpdataState(id, 1);
                            }

                        }
                    }
                    catch {
                    } 
                }
                else {
                    view.State = -1;
                    view.MALU_Code = "";
                }
               
            }
            return this.JsonContent(sbl);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="mms"></param>
        /// <returns></returns>
        [HttpPost]
        //return 1 是成功，2是验证码错误，3 失败
        public ContentResult AddUser(AddMembersInput mms)
        {
            if (this.CreateService<IMembersAppService>().IsReg(mms.IdCard) > 0) {
                return this.JsonContent(1);
            }
            string code = WebHelper.GetSession<string>(SMSCodeKey);
            WebHelper.RemoveSession(SMSCodeKey);
            if (code.IsNullOrEmpty() || code.ToLower() != mms.PhoneVcode)
            {
                return this.JsonContent(2);
            }
            return this.JsonContent(this.CreateService<IMembersAppService>().Add(mms));
        }

        /// <summary>
        /// 预约排队
        /// </summary>
        /// <param name="aapp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAppointmentTran(AddAppointmentDataInput aapp)
        {
            try
            {
                if (this.CreateService<IAppointmentDataService>().CancelAppNum(aapp.MamberID) >= Int32.Parse(this.CreateService<IDefaultSettingService>().GetKeyValueForName("每月可取消预约次数")))
                    return this.JsonContent(-2);
            }
            catch { }
            try
            {
                if(!this.CreateService<IAppointmentDataService>().HasAppointment(aapp))
                    return this.JsonContent(-3);//已经预约过同样的业务
            }
            catch { }
            int SERN=0; string StartTime=""; string EndTime=""; string BusName = "";
            this.CreateService<IBusinessService>().GetSerNo(aapp.BusinessID,ref SERN,ref StartTime,ref EndTime,ref BusName);
            MALU_Members mm = this.CreateService<IMembersAppService>().GetInfoByID(aapp.MamberID);

            WebService.WebService ss = new WebService.WebService();
            string BookID=ss.AddApp(aapp.AppointmentDate.Value, SERN, mm.IdCard, mm.Name, mm.MobilePhone, StartTime, EndTime);
            aapp.BookingNo = BookID;
            AppointmentData appd = this.CreateService<IAppointmentDataService>().Add(aapp);
            //SMS.WebService SW = new SMS.WebService();
       
            //SW.sendsms(mm.MobilePhone, "您好！你已成功预约【" + aapp.AppointmentDate.Value.ToShortDateString()+" "+ StartTime +"-"+ EndTime+ "】【" + BusName + "】业务，请在预约的时间段提前半小时到达现场取号办理，咨询电话0755-82092409。", "7550D0F39DA9350B26433CA6FA8EF24F");

            return this.JsonContent(appd);
        }
        [HttpPost]
        public int AddEvaluate(int Star, string Evaluation,string ApponitID,string Mid) {
            Evaluate obj = new Evaluate();
            obj.ApponitID = ApponitID;
            obj.Evaluation = Evaluation;
            obj.MenberID = Mid;
            obj.Star = Star;
            if (this.CreateService<IEvaluateService>().Add(obj).Id != "")
            {
                this.CreateService<IAppointmentDataService>().Eval(ApponitID);
                return 1;
            }
            else {
                return 0;
            }
        }

        [HttpGet]
        public ActionResult GetSet()
        {
            List<MALU_DefaultSetting> dss = this.CreateService<IDefaultSettingService>().SelectFor();
            return this.JsonContent(dss);
        }

 
        /// <summary>
        /// 排队取号
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        //[HttpGet]
        //public string TakeNo(string UserId)
        //{ 
        //    WebService.WebService ws = new WebService.WebService();
        //    ws.GetTicketCode(view.SerNo, IDCard);
        //    return "";
        //}
        [HttpGet]
        public int IsAppoint(string TanID,string UserID)
        {
           return this.CreateService<IAppointmentDataService>().IsApp(TanID,UserID);
        }
        [HttpGet]
        public int CancelAppNum(string UserID)
        {
            return this.CreateService<IAppointmentDataService>().CancelAppNum(UserID);
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
        #region test
        /// <summary>
        /// test
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public ContentResult GetUserInfoTest(string code)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", AppID_WeChat, AppSecret_WeChat, code);
            string atstring = HttpClientHelper.GetResponse(url);
            _Access_Token at = JsonConvert.DeserializeObject<_Access_Token>(atstring);

            url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", at.access_token, at.openid);
            string UserInfo = HttpClientHelper.GetResponse(url);

            return this.JsonContent(UserInfo);
        }
        [HttpGet]
        public void test()
        {
            WebService.WebService ss = new WebService.WebService();
            DateTime t = DateTime.Now;
            string BookID = ss.AddApp(t, 1, "123456", "test", "123456", "", "");
        }
        #endregion

        public class HttpClientHelper
        {
            /// <summary>
            ///     get请求
            /// </summary>
            /// <param name="url"></param>
            /// <returns></returns>
            public static string GetResponse(string url)
            {
                if (url.StartsWith("https"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                }

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
                return null;
            }

            public static T GetResponse<T>(string url)
                where T : class, new()
            {
                if (url.StartsWith("https"))
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                T result = default(T);

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;

                    result = JsonConvert.DeserializeObject<T>(s);
                }
                return result;
            }

            /// <summary>
            ///     post请求
            /// </summary>
            /// <param name="url"></param>
            /// <param name="postData">post数据</param>
            /// <returns></returns>
            public static string PostResponse(string url, string postData)
            {
                if (url.StartsWith("https"))
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                HttpContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var httpClient = new HttpClient();

                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
                return null;
            }

            /// <summary>
            ///     发起post请求
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="url">url</param>
            /// <param name="postData">post数据</param>
            /// <returns></returns>
            public static T PostResponse<T>(string url, string postData)
                where T : class, new()
            {
                if (url.StartsWith("https"))
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                HttpContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var httpClient = new HttpClient();

                T result = default(T);

                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;

                    result = JsonConvert.DeserializeObject<T>(s);
                }
                return result;
            }

            /// <summary>
            ///     V3接口全部为Xml形式，故有此方法
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="url"></param>
            /// <param name="xmlString"></param>
            /// <returns></returns>
            public static T PostXmlResponse<T>(string url, string xmlString)
                where T : class, new()
            {
                if (url.StartsWith("https"))
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                HttpContent httpContent = new StringContent(xmlString);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var httpClient = new HttpClient();

                T result = default(T);

                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;

                    result = XmlDeserialize<T>(s);
                }
                return result;
            }

            /// <summary>
            ///     反序列化Xml
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="xmlString"></param>
            /// <returns></returns>
            public static T XmlDeserialize<T>(string xmlString)
                where T : class, new()
            {
                try
                {
                    var ser = new XmlSerializer(typeof(T));
                    using (var reader = new StringReader(xmlString))
                    {
                        return (T)ser.Deserialize(reader);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("XmlDeserialize发生异常：xmlString:" + xmlString + "异常信息：" + ex.Message);
                }
            }

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
            public string headimgurl { get; set; }

            public string MobilePhone { get; set; }

            public string IdCard { get; set; }
            public string Name { get; set; }

            public string Id { get; set; }
            public string ReValue { get; set; }

            public int State { get; set; }

            public int CancelNum { get; set; }
        }
    }
}