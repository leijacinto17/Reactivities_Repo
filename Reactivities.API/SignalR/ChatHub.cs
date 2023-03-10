using Microsoft.AspNetCore.SignalR;
using Reactivities.Application.Models.Comments;
using Reactivities.Application.Services.Comments;

namespace Reactivities.API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly ICommentServices _commentServices;
        public ChatHub(ICommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        public async Task SendComment(CommentsValues comments)
        {
            //insert comment
            var comment = await _commentServices.InsertCommentsAsync(comments);
            //Send the comment to ReceiveComment method and create a group based on the activity id
            await Clients.Group(comments.ActivityId.ToString())
                         .SendAsync("ReceiveComment", comment.Value);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            //get the activity id in the parameter
            var activityId = httpContext.Request.Query["activityId"];
            //Create a group for the connection 
            await Groups.AddToGroupAsync(Context.ConnectionId, activityId);
            //return the comments
            var result = await _commentServices.GetCommentsAsync(Guid.Parse(activityId));
            //Send the result in the LoadComments Method
            await Clients.Caller.SendAsync("LoadComments", result.Value);
        }
    }
}
