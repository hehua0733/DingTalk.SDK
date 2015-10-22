using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Entities
{
    public class JsonResult
    {
        public ReturnCode errcode { get; set; }
        public string errmsg { get; set; }
    }
    public class GetTokenResult : JsonResult
    {
        public string access_token { get; set; }

    }
    /// <summary>
    /// 发送普通会话消息返回
    /// </summary>
    public class SendMessageResult : JsonResult
    {

        public string invaliduser { get; set; }
        public string invalidparty { get; set; }

        public string receiver { get; set; }
        public string[] receivers
        {
            get
            {
                if (!string.IsNullOrEmpty(receiver))
                {
                    return receiver.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                }
                return null;
            }
        }

    }

    public class UploadMediaResult : JsonResult
    {
        public string type { get; set; }
        public string media_id { get; set; }
        public string created_at { get; set; }

    }

    public class CreateDepartmentResult : JsonResult
    {
        public int id { get; set; }
    }
    public class CreateUserResult : JsonResult
    {
        public string userid { get; set; }
    }

    public class SimpleListResult : JsonResult
    {
        public UserInfo[] userlist { get; set; }

    }
}
