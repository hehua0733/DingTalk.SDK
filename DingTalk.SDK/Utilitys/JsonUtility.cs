using DingTalk.SDK.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Utilitys
{
    public class JsonUtility
    {
        public static string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj,
                Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings
                { NullValueHandling = NullValueHandling.Ignore });
        }

        public static T Deserialize<T>(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }


    }
}
