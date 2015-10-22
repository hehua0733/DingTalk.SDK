using DingTalk.SDK.Entities;
using DingTalk.SDK.Utilitys.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.DD
{
    public class Media
    {
        private const string URL_GETMEDIA = "https://oapi.dingtalk.com/media/get?access_token={0}&media_id={1}";
        private const string URL_UPLOAD = "https://oapi.dingtalk.com/media/upload?access_token={0}&type={1}";

        #region GET

        public static void Get(string media_id, string filepath)
        {
            Get(new Entities.Media { media_id = media_id }, filepath);
        }


        public static void Get(Entities.Media media, string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                new System.IO.IOException(filepath + "已经存在。");
            }
            System.IO.FileStream stream = new System.IO.FileStream(filepath, System.IO.FileMode.CreateNew);
            Utilitys.HttpUtility.Get.Download(string.Format(URL_GETMEDIA, Auth.GetToken(), media.media_id), stream);
        }
        public static System.IO.MemoryStream Get(string media_id)
        {
            return Get(new Entities.Media { media_id = media_id });
        }


        public static System.IO.MemoryStream Get(Entities.Media media)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            Utilitys.HttpUtility.Get.Download(string.Format(URL_GETMEDIA, Auth.GetToken(), media.media_id), stream);
            return stream;
        }
        #endregion

        public static UploadMediaResult Upload(string filepath)
        {
            if (!System.IO.File.Exists(filepath))
                throw new System.IO.IOException(filepath + "文件不存在");

            System.IO.FileInfo file = new System.IO.FileInfo(filepath);
            var ext = System.IO.Path.GetExtension(filepath);
            switch (ext)
            {
                case ".amr":
                    ext = "voice";
                    if (file.Length > 1024 * 1024)//1M
                    {
                        ext = "file";
                    }
                    break;
                case ".jpg":
                    ext = "image";
                    if (file.Length > 1024 * 1024 * 2)//2M
                    {
                        ext = "file";
                    }
                    break;
                default:
                    ext = "file";
                    break;
            }
          return  Post.PostFile<UploadMediaResult>(string.Format(URL_UPLOAD, Auth.GetToken(), ext), filepath);
        }


    }


}
