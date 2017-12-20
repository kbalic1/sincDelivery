using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace sincDelivery.Helpers
{
    public class NotificationHub : Hub
    {
        public void Send()
        {
            Clients.All.SendNotification("Notification");
        }
    }
}