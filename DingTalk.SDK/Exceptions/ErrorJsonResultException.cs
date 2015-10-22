using DingTalk.SDK.Entities;
using DingTalk.SDK.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Exceptions
{
    public class ErrorJsonResultException : DDException
    {
        public JsonResult Result { get; private set; }
        public string Url { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        /// <param name="jsonResult"></param>
        /// <param name="url"></param>
        public ErrorJsonResultException(string message, Exception inner, JsonResult jsonResult, string url = null)
            : base(message, inner)
        {
            Result = jsonResult;
            Url = url;

            LogUtility.Debug(message);
        }
    }
}
