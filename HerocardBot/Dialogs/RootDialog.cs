using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HerocardBot.Dialogs
{
  [Serializable]
  public class RootDialog : IDialog<object>
  {
    public Task StartAsync(IDialogContext context)
    {
      context.Wait(MessageReceivedAsync);

      return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
    {
      var message = await result;
      var welcomeMessage = context.MakeMessage();
      welcomeMessage.Text = "Welcome To the hero card bot";

      

      // Return our reply to the user
      await context.PostAsync(welcomeMessage);

      await this.DisplayHeroCard(context);

      context.Wait(MessageReceivedAsync);
    }

    public async Task DisplayHeroCard(IDialogContext context)
    {
      var replyMessage = context.MakeMessage();
      Attachment attachment = new HeroCard {
        Title = "Krishna's card",
        Subtitle = "Lord of the Universe",
        Text = "Krishna is the great Lord of India",
        Tap = new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://en.wikipedia.org/wiki/Krishna"),
        Images = new List<CardImage> { new CardImage("https://i.pinimg.com/236x/9f/50/29/9f5029a2c808f5114704612689337f81--krishna-cgi.jpg") },
        Buttons=new List<CardAction> { new CardAction(ActionTypes.OpenUrl,"Learn More", value: "https://en.wikipedia.org/wiki/Krishna") }
        

      }.ToAttachment();
      replyMessage.Attachments = new List<Attachment> { attachment };
      await context.PostAsync(replyMessage);
    }
  }
}
