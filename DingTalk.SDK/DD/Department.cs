using DingTalk.SDK.Entities;
using DingTalk.SDK.Utilitys.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.DD
{
    public class Department
    {
        private const string URL_LIST = "https://oapi.dingtalk.com/department/list?access_token={0}";
        private const string URL_CREATE = "https://oapi.dingtalk.com/department/create?access_token={0}";
        private const string URL_UPDATE = "https://oapi.dingtalk.com/department/update?access_token={0}";
        private const string URL_DELETE = "https://oapi.dingtalk.com/department/delete?access_token={0}&id={1}";


        public static List<Entities.Department> list()
        {

            var result = Get.GetJson<ListDepartmentResult>(string.Format(URL_LIST, Auth.GetToken()));
            if (result.errcode == ReturnCode._请求成功)
            {
                return result.department;
            }
            return null;
        }

        public static long create(string name, int parentid, string order = "", bool createDeptGroup = false)
        {

            var result = Post.PostJson<CreateDepartmentResult>(string.Format(URL_CREATE, Auth.GetToken()),
                new Entities.Department { name = name, parentid = parentid, order = order, createDeptGroup = createDeptGroup });
            if (result.errcode == ReturnCode._请求成功)
            {
                return result.id;
            }
            return 0;
        }


        public static bool update(Entities.Department dept)
        {
            var result = Post.PostJson<JsonResult>(string.Format(URL_UPDATE, Auth.GetToken()), dept);
            return result.errcode == ReturnCode._请求成功;
        }

        public static bool delete(long id)
        {
            var result = Get.GetJson<JsonResult>(string.Format(URL_DELETE, Auth.GetToken(), id));
            return result.errcode == ReturnCode._请求成功;
        }

    }
}
