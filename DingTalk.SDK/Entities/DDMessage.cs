using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Entities
{

    public class DDTextMessage : DDMessage
    {
        public DDTextMessage(string content) : base("text")
        {
            this.text = new Conetnt { content = content };
        }

        public Conetnt text { get; set; }
        public class Conetnt
        {
            public string content { get; set; }
        }
    }
    #region 多媒体消息
    public class DDImageMessage : DDMessage
    {
        public DDImageMessage(string media_id) : base("image")
        {
            this.image = new Media { media_id = media_id };
        }
        public Media image { get; set; }
    }
    public class DDVoiceMessage : DDMessage
    {
        public DDVoiceMessage(string media_id) : base("voice")
        {
            this.voice = new Media { media_id = media_id };
        }
        public Media voice { get; set; }
    }
    public class DDFileMessage : DDMessage
    {
        public DDFileMessage(string media_id) : base("file")
        {
            this.file = new Media { media_id = media_id };
        }
        public Media file { get; set; }
    }
    public class Media
    {
        public string media_id { get; set; }

    }




    public class DDLinkMessage : DDMessage
    {
        public DDLinkMessage(string messageUrl, string picUrl, string title, string text) : base("link")
        {
            this.link = new Link
            {
                messageUrl = messageUrl,
                picUrl = picUrl,
                text = text,
                title = title
            };
        }
        public Link link { get; set; }
        public class Link
        {
            public string messageUrl { get; set; }
            public string picUrl { get; set; }
            public string title { get; set; }
            public string text { get; set; }
        }
    }

    #region OA Message

    public class DDOAMessage : DDMessage
    {
        public DDOAMessage() : base("oa")
        {

        }

        public string message_url { get; set; }

        public OA oa { get; set; }
        public class OA
        {

            public Head head { get; set; }
            public Body body { get; set; }
            public class Head
            {
                public string bgcolor { get; set; }
                public string text { get; set; }

            }

            public class Body
            {
                public string title { get; set; }
                public string content { get; set; }
                public string image { get; set; }
                public string file_count { get; set; }
                public string author { get; set; }


                public FormKeyValue[] form { get; set; }
                public Rich rich { get; set; }

                public class FormKeyValue 
                { 
                    public string key { get; set; }
                    public string value { get; set; }
                }
                public class Rich
                {
                    public string unit { get; set; }
                    public string num { get; set; }

                }

            }
        }

    }


    #endregion

    #endregion

    public interface IConversationMessage
    {
        string sender { get; set; }
        string cid { get; set; }
    }


    public interface IEnterpriseMessage
    {
        string touser { get; set; }
        string toparty { get; set; }
        string agentid { get; set; }
    }



    public class DDMessage : IConversationMessage, IEnterpriseMessage

    {

        public string toparty { get; set; }

        public string touser { get; set; }
        public string agentid { get; set; }

        public string cid { get; set; }

        public string sender { get; set; }


        public string msgtype { get; set; }
        public DDMessage(string _msgtype)
        {
            this.msgtype = _msgtype;
        }
    }




}
