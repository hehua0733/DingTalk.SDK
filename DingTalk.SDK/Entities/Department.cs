using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Entities
{
    public class ListDepartmentResult : JsonResult
    {
        public List<Department> department { get; set; }
    }

    public class Department
    {
        public long id { get; set; }
        public string name { get; set; }
        public int parentid { get; set; }




        public string order { get; set; }
        /// <summary>
        /// 是否创建一个关联此部门的企业群
        /// </summary>
        public bool createDeptGroup { get; set; } 
        /// <summary>
        /// 如果有新人加入部门是否会自动加入部门群
        /// </summary>
        public bool autoAddUser { get; set; }
        /// <summary>
        /// 部门的主管列表,取值为由主管的userid组成的字符串，不同的userid使用’ | ‘符号进行分割
        /// </summary>
        public string deptManagerUseridList { get; set; }
        /// <summary>
        /// 是否隐藏部门
        /// </summary>
        public bool deptHiding { get; set; }
        /// <summary>
        /// 可以查看指定隐藏部门的其他部门列表，如果部门隐藏，则此值生效，取值为其他的部门ID组成的的字符串，使用’ | '符号进行分割
        /// </summary>
        public string deptPerimits { get; set; }

    }
}
