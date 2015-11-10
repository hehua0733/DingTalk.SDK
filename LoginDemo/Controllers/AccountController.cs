﻿using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
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
    public class AccountController : Controller
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
                return View();
            }
        }



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
            //二维码是否失效
            Guid g = CacheUtil.GetCache<Guid>("T_" + q);
            if (g == null || Guid.Empty == g)
            {
                return View();
            }



            if (string.IsNullOrEmpty(state))
            {
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

        public ActionResult QR()
        {

            Guid g = Guid.NewGuid();
            CacheUtil.PutCache("T_" + g, g, 60000);
            QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.L);
            QrCode qrCode;
            encoder.TryEncode("http://mobiledev.scrzjh.com:9999/l/" + g.ToString(), out qrCode);
            MemoryStream ms = new MemoryStream();
            var render = new GraphicsRenderer(new FixedModuleSize(8, QuietZoneModules.Two));
            render.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            return File(ms.GetBuffer(), "image/png");
        }
    }
}