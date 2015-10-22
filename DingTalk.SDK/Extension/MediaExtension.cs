using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Extension
{
    public static class MediaExtension
    {
        public static void Download(this Entities.Media media, string path) {
            DD.Media.Get(media, path);
        }
    }
}
