using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Entities
{
    public class UserInfo : JsonResult
    {
        public string dingId
        {
            get; set;
        }
        public string userid
        {
            get; set;
        }
        public string deviceId
        {
            get; set;
        }
        public string is_sys
        {
            get; set;
        }
        public string sys_level
        {
            get; set;
        }
        public bool isAdmin { get; set; }
        public bool isHide { get; set; }
        public bool isLeader { get; set; }
        public bool active { get; set; }


        public string email { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public int[] department { get; set; }

        public string position { get; set; }
        public string avatar { get; set; }
        public string jobnumber { get; set; }

        public Extattr extattr { get; set; }



        public class Extattr
        {
            public class NameValuePair
            {
                public string name { get; set; }
                public string value { get; set; }
            }

            public NameValuePair[] attrs { get; set; }


        }


    }
}
