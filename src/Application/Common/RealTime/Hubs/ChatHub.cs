 
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tawala.Application.Common.RealTime.Hubs
{
    public class ChatHub : Hub
    {

        public static List<ConecectedUsers> callCenterUsers = new List<ConecectedUsers>();
        public static List<ConecectedUsers> Users = new List<ConecectedUsers>();

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = callCenterUsers.Where(x => x.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (user != null)
            {
                callCenterUsers.Remove(user);
            } ;


            var user2 =  Users.Where(x => x.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (user2 != null)
            {
                Users.Remove(user2);
            };



            Clients.Group("CallCenter").SendAsync("JoinToGroup", user.UserId, callCenterUsers, "Disconnect", Users);
            return base.OnDisconnectedAsync(exception);
        }


        public Task SendMessageToGroup(string groupName, string message, string other)
        {
            return Clients.Group(groupName).SendAsync("ReceiveMessage", $"{groupName}", other);
        }

        //public Task SendTaskToGroup(string groupName, string message, string other)
        //{
        //    return Clients.Group(groupName).SendAsync("ReceiveTask", $"{groupName}", other);
        //}

        public async Task AddToGroup(string groupName, string UserId, bool isCallcenter)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            if (isCallcenter)
            {

                callCenterUsers.Add(new ConecectedUsers()
                {
                    UserId = UserId,
                    ConnectionId = Context.ConnectionId,

                });
            }
            else {

                Users.Add(new ConecectedUsers()
                {
                    UserId = UserId,
                    ConnectionId = Context.ConnectionId,

                });
            }

            await Clients.Group(groupName).SendAsync("JoinToGroup", UserId, callCenterUsers, "Connect", Context.ConnectionId, Users);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            var user = callCenterUsers.Where(x => x.ConnectionId == Context.ConnectionId).FirstOrDefault();
            if (user != null)
            {
                callCenterUsers.Remove(user);
            }
            await Clients.Group(groupName).SendAsync("JoinToGroup", user.UserId, callCenterUsers, "Disconnect");
        }

        public async Task SendPrivateMessage(string senderId,string reciverId , string message)
        {
            await Clients.Client(reciverId).SendAsync("ReceiveMessage", message, senderId);
        }
    }
    public class ConecectedUsers
    {
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
    }
}
