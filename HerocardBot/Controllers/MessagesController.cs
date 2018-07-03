using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HerocardBot
{
  [BotAuthentication]
  public class MessagesController : ApiController
  {
    /// <summary>
    /// POST: api/Messages
    /// Receive a message from a user and reply to it
    /// </summary>
    public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
    {
      if (activity.GetActivityType() == ActivityTypes.Message)
      {
        await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
      }
      else
      {
        HandleSystemMessage(activity);
      }
      var response = Request.CreateResponse(HttpStatusCode.OK);
      return response;
    }

    private Activity HandleSystemMessage(Activity message)
    {
      string messageType = message.GetActivityType();
      if (messageType == ActivityTypes.DeleteUserData)
      {
        // Implement user deletion here
        // If we handle user deletion, return a real message
      }
      else if (messageType == ActivityTypes.ConversationUpdate)
      {
        message.Text = "welcome to bot framework";
        return message;
      }
      else if (messageType == ActivityTypes.ContactRelationUpdate)
      {
        // Handle add/remove from contact lists
        // Activity.From + Activity.Action represent what happened
      }
      else if (messageType == ActivityTypes.Typing)
      {
        message.Text = "typing...";
        return message;
      }
      else if (messageType == ActivityTypes.Ping)
      {
        
      }

      return null;
    }
  }
}
