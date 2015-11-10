using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using LoginDemo.Models;
using LoginDemo.Utils;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LoginDemo.Controllers
{
    public class AccountController : AsyncController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            var cookie = Request.Cookies.Get("_ASPX_QR_AUTH");
            if (cookie != null)
            {
                var usercookie = FormsAuthentication.Decrypt(cookie.Value);
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<DingTalk.SDK.Entities.UserInfo>(usercookie.UserData);
                return View(user);
            }
            else
            {

                ViewBag.Id = Guid.NewGuid();
                ViewBag.Qr = Base64.Base64Encode(ViewBag.Id + "");

                QRScanModel qm = new QRScanModel { Id = ViewBag.Id, IsComfirm = false, Qr = Base64.Base64Encode(ViewBag.Id.ToString()) };
                qm.ExpiredTime = DateTime.Now.AddMilliseconds(300000);
                CacheUtil.PutCache("T_" + qm.Id, qm, 300000);


                return View();
            }
        }
        public ActionResult QR(string id)
        {
            QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.L);
            QrCode qrCode;
            encoder.TryEncode("http://mobiledev.scrzjh.com:9999/l/" + id.ToString(), out qrCode);
            MemoryStream ms = new MemoryStream();
            var render = new GraphicsRenderer(new FixedModuleSize(8, QuietZoneModules.Two));
            render.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var qm = CacheUtil.GetCache<QRScanModel>("T_" + Base64.Base64Decode(id));
            if (qm != null)
            {
                qm.QrGenerateTime = DateTime.Now;
                TimeSpan expiredTime = qm.ExpiredTime - DateTime.Now;
                CacheUtil.PutCache("T_" + qm.Id, qm, (int)expiredTime.TotalMilliseconds);
            }
            return File(ms.GetBuffer(), "image/png");
        }

        #region 轮询长链接


        // 处理客户端发起的请求
        public void PollingAsync(Guid id, bool isScaned = false)
        {


            //计时器，5秒种触发一次Elapsed事件
            System.Timers.Timer timer = new System.Timers.Timer(5000);
            //告诉ASP.NET接下来将进行一个异步操作
            AsyncManager.OutstandingOperations.Increment();
            //订阅计时器的Elapsed事件
            timer.Elapsed += (sender, e) =>
            {

                //打开一个监听 ，发现参数变化时，立即返回值?????


                AsyncManager.Parameters["id"] = id;
                AsyncManager.Parameters["timer"] = sender as System.Timers.Timer;

                AsyncManager.OutstandingOperations.Decrement();
            };

            //启动计时器
            timer.Start();

            var qm = CacheUtil.GetCache<QRScanModel>("T_" + id);
            if (qm != null && timer == null)
            {
                qm.PropertyChanged += (sender, e) =>
                {
                    AsyncManager.OutstandingOperations.Decrement();
                };

                qm.Timer = timer;
                TimeSpan expiredTime = qm.ExpiredTime - DateTime.Now;
                CacheUtil.PutCache("T_" + qm.Id, qm, (int)expiredTime.TotalMilliseconds);
            }


        }

        //异步处理完成，向客户端发送响应
        public ActionResult PollingCompleted(Guid id, System.Timers.Timer timer)
        {


            var qm = CacheUtil.GetCache<QRScanModel>("T_" + id);

            if (qm != null)
            {
                if (qm.IsScan)
                {
                    return Json(new
                    {
                        status = 201,

                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (DateTime.Now - qm.QrGenerateTime > new TimeSpan(0, 1, 0))
                    {
                        //更新QR内容
                        CacheUtil.RemoveCache(qm.Id + "");
                        qm.Id = Guid.NewGuid();
                        qm.Qr = Base64.Base64Encode(qm.Id.ToString());
                        qm.QrGenerateTime = DateTime.Now;
                        TimeSpan expiredTime = qm.ExpiredTime - DateTime.Now;
                        CacheUtil.PutCache("T_" + qm.Id, qm, (int)expiredTime.TotalMilliseconds);

                        return Json(new
                        {
                            status = 409,
                            id = qm.Id,
                            qr = qm.Qr

                        }, JsonRequestBehavior.AllowGet);
                    }


                    return Json(new
                    {
                        status = 408,

                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                status = 400,

            }, JsonRequestBehavior.AllowGet);

        }
        #endregion


        /// <summary>
        /// 这个页面是在手机上打开的
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginConfirm()
        {
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
            //   user.name,
            //   DateTime.Now,
            //   DateTime.Now,
            //   false,
            //   user.userid + "",
            //   "/");
            //var logincookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            //logincookie.HttpOnly = true;
            //HttpContext.Response.Cookies.Add(logincookie);

            return View();
        }


        private readonly string CORPID = System.Configuration.ConfigurationManager.AppSettings["DD_CORPID"] + "";
        private readonly string CORPSECRET = System.Configuration.ConfigurationManager.AppSettings["DD_CORPSECRET"] + "";

        /// <summary>
        /// 这个页面是在手机上打开的
        /// </summary>
        /// <param name="q"></param>
        /// <param name="state"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult LoginWithQR(string q, string state = "", string code = "")
        {




            if (string.IsNullOrEmpty(state))
            {

                //二维码是否失效
                QRScanModel g = CacheUtil.GetCache<QRScanModel>("T_" + Base64.Base64Decode(q));
                if (g == null)
                {
                    return View("empty");
                }


                var url = string.Format("https://oapi.dingtalk.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=SCOPE&state=STATE",
                    CORPID,
                    Server.UrlEncode("http://mobiledev.scrzjh.com:9999/l/" + q)
                    );
                return Redirect(url);
            }
            else if (!string.IsNullOrEmpty(state) && !string.IsNullOrEmpty(code))
            {
                var user = DingTalk.SDK.DD.User.getuserinfo(code);
                user = DingTalk.SDK.DD.User.get(user.userid);
                ViewBag.JSON = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                if (user != null)
                {
                    return View("LoginConfirm", user);
                }
            }

            return View();
        }

        public ActionResult Test(Guid id)
        {
            QRScanModel g = CacheUtil.GetCache<QRScanModel>("T_" + id);

            if (g != null)
                g.IsScan = true;

            return Content("");
        }



    }
}