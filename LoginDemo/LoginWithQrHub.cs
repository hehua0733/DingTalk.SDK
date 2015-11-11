
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace LoginDemo
{
    [HubName("QrHub")]
    public class LoginWithQrHub : Hub
    {
        
        public void Scaned(string pcConnectionId, string mobileConnectionId, string avatar, string username)
        {
            Clients.Client(pcConnectionId).LetConfirm(avatar, username);
        }
        public void Confirm(string pcConnectionId, string mobileConnectionId, string userid)
        {
            Clients.Client(pcConnectionId).Login(userid);

            Clients.Client(pcConnectionId).Disconnect();
            Clients.Client(mobileConnectionId).Disconnect();
        }
    }
}