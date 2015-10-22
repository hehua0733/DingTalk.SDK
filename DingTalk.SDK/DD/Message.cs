using DingTalk.SDK.Entities;
using DingTalk.SDK.Utilitys.HttpUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.DD
{
    public class Message
    {
        private const string URL_send_to_conversation = "https://oapi.dingtalk.com/message/send_to_conversation?access_token={0}";
        private const string URL_send = "https://oapi.dingtalk.com/message/send?access_token={0}";
        /// <summary>
        /// 发送普通会话消息
        /// 员工可以在微应用中把消息发送到同企业的人或群。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static SendMessageResult send_to_conversation(string sender,
            string cid, string agentid, DDMessage msg)
        {
            var token = Auth.GetToken();
            msg.sender = sender; msg.cid = cid; msg.agentid = agentid;
            var rst = Post.PostJson<SendMessageResult>(string.Format(URL_send_to_conversation, token), msg);
            return rst;
        }

        /// <summary>
        /// 发送企业会话消息
        /// 企业可以主动发消息给员工，消息量不受限制。
        /// 发送企业会话消息和发送普通会话消息的不同之处在于发送消息的主体不同 - 普通会话消息发送主体是普通员工，体现在接收方手机上的联系人是消息发送员工
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="toparty"></param>
        /// <param name="agentid"></param>
        /// <returns></returns>
        public static SendMessageResult send(string touser, string toparty, string agentid, DDMessage msg)
        {
            var token = Auth.GetToken();
            msg.touser = touser;
            msg.toparty = toparty;
            msg.agentid = agentid;
            var rst = Post.PostJson<SendMessageResult>(string.Format(URL_send, token), msg);
            return rst;
        }
    }
}
