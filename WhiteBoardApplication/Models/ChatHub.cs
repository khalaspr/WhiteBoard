using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhiteBoardApplication.Models
{
    public class ChatHub:Hub
    {
        public void Send(string name, string message,string IsRealTime)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message, IsRealTime);
        }
    }
}