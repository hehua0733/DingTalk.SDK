using DingTalk.SDK.Entities;
using DingTalk.SDK.Utilitys.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.DD
{
    public class User
    {
        private const string URL_GETUSERINFO_CODE = "https://oapi.dingtalk.com/user/getuserinfo?access_token={0}&code={1}";
        private const string URL_GETUSERINFO_USERID = "https://oapi.dingtalk.com/user/get?access_token={0}&userid={1}";
        private const string URL_CREATE = "https://oapi.dingtalk.com/user/create?access_token={0}";
        private const string URL_DELETE = "https://oapi.dingtalk.com/user/delete?access_token={0}&userid={1}";
        private const string URL_BATCHDELETE = "https://oapi.dingtalk.com/user/batchdelete?access_token={0}";
        private const string URL_UPDATE = "https://oapi.dingtalk.com/user/update?access_token={0}";
        private const string URL_SIMPLELIST = "https://oapi.dingtalk.com/user/simplelist?access_token={0}&department_id={1}";
        private const string URL_LIST = "https://oapi.dingtalk.com/user/list?access_token={0}&department_id={1}";
        public static UserInfo getuserinfo(string code)
        {
            return Get.GetJson<UserInfo>(string.Format(URL_GETUSERINFO_CODE, Auth.GetToken(), code));
        }


        public static UserInfo get(string userid)
        {
            return Get.GetJson<UserInfo>(string.Format(URL_GETUSERINFO_USERID, Auth.GetToken(), userid));
        }
        public static string create(UserInfo user)
        {
            var result = Post.PostJson<CreateUserResult>(string.Format(URL_CREATE, Auth.GetToken()), user);
            if (result != null)
            {
                return result.userid;
            }
            return string.Empty;
        }

        public static bool delete(string userid)
        {
            var result = Get.GetJson<JsonResult>(string.Format(URL_DELETE, Auth.GetToken(), userid));
            return result.errcode == ReturnCode._请求成功;
        }

        public static bool update(UserInfo user)
        {
            var result = Post.PostJson<JsonResult>(string.Format(URL_UPDATE, Auth.GetToken()), user);
            return result.errcode == ReturnCode._请求成功;
        }

        /// <summary>
        /// 指删除用户，最多一次删除20条
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool batchdelete(string[] user)
        {
            var result = Post.PostJson<JsonResult>(string.Format(URL_BATCHDELETE, Auth.GetToken()), new { useridlist = user.Take(20) });
            return result.errcode == ReturnCode._请求成功;
        }



        public static UserInfo[] simplelist(long department_id)
        {
            return list(department_id);
        }
        public static UserInfo[] list(long department_id)
        {
            var result = Get.GetJson<SimpleListResult>(string.Format(URL_LIST, Auth.GetToken(), department_id));
            if (result.errcode == ReturnCode._请求成功)
            {
                return result.userlist;
            }
            return null;
        }
    }
}
