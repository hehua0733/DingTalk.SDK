using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace LoginDemo.Utils
{
    public class CacheUtil
    {

        static ObjectCache _cache;

        static ObjectCache m_Cache
        {
            get
            {
                if (_cache == null)
                {
                    _cache = MemoryCache.Default;
                }

                return _cache;
            }
        }
        public static V GetCache<V>(string key)
        {

            if (m_Cache.Contains(key))
            {
                return (V)m_Cache[key];
            }
            else
            {
                return default(V);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryTime">过期时间 单位：ms</param>
        public static void PutCache(string key, object value, int expiryTime = 1200000)
        {

            CacheItemPolicy _policy = new CacheItemPolicy();
            _policy.AbsoluteExpiration = DateTime.Now.AddMilliseconds(expiryTime);

            m_Cache.Set(key, value, _policy);
        }

        public static void RemoveCache(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                m_Cache.Remove(key);
            }
        }

    }
}