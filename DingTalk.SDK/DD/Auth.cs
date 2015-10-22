using DingTalk.SDK.Entities;
using DingTalk.SDK.Utilitys;
using DingTalk.SDK.Utilitys.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.DD
{
    public class Auth
    {
        private const string URL_GETTOKEN = "https://oapi.dingtalk.com/gettoken?corpid={0}&corpsecret={1}";


        public static string GetToken()
        {
            var cacheKEY = "TOKEN_" + Config.CORPID + Config.CORPSECRET;
            var cache = CacheUtility.GetCache<string>(cacheKEY);

            if (cache == null)
            {
                var token = Get.GetJson<GetTokenResult>(string.Format(URL_GETTOKEN, Config.CORPID, Config.CORPSECRET));

                if (token != null)
                {
                    cache = token.access_token;
                    CacheUtility.PutCache(cacheKEY, cache, 7000);
                }
            }
            Console.WriteLine("TOKEN:" + cache);
            return cache;
        }
    }
}
