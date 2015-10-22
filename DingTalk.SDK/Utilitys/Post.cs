/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：Post.cs
    文件功能描述：Post
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
 
    修改标识：zhanghao-kooboo - 20150316
    修改描述：增加
 
    修改标识：Senparc - 20150407
    修改描述：发起Post请求方法修改，为了上传永久视频素材
----------------------------------------------------------------*/

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