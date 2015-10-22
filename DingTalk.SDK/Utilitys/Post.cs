using DingTalk.SDK.Entities;
using DingTalk.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace DingTalk.SDK.Utilitys.HttpUtility
{
    public static class Post
    {
        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        private static T GetResult<T>(string returnText)
            where T : JsonResult
        {

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                JsonResult errorResult = JsonUtility.Deserialize<JsonResult>(returnText);
                if (errorResult.errcode != ReturnCode._请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                        string.Format("Post请求发生错误！错误代码：{0}，说明：{1}",
                                      (int)errorResult.errcode,
                                      errorResult.errmsg),
                        null, errorResult);
                }
            }
            return JsonUtility.Deserialize<T>(returnText);
        }



        public static T PostFile<T>(string url, string files) where T : JsonResult
        {
            string returnText = RequestUtility.PostFile(url, files, 20000);
            var result = GetResult<T>(returnText);
            return result;
        }


        public static T PostJson<T>(string url, object obj, int timeOut = Config.TIME_OUT)
         where T : JsonResult
        {
            string returnText = RequestUtility.HttpPost(url, JsonUtility.Serialize(obj), timeOut);
            var result = GetResult<T>(returnText);
            return result;
        }





    }
}