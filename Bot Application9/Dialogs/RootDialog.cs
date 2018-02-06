using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application9.Dialogs {
    [Serializable]
    

    public class RootDialog : IDialog<object> {

        string[] data = new string[2] {"", ""};
        string[] inst = new string[2] { "Tell your name to the fortune teller to know your future", "Where are you from?"};
        string[] future = new string[4] { "try to stay away from wild fires", "try to stay away from wild lions"
                                          , "try to stay away from wild goose", "try to stay away from tornadoes" };
        static Random rnd = new Random();

        int data_count = 0;
        int inst_count = 0;

        public Task StartAsync(IDialogContext context) {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result) {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            if (data_count == 0) {
                await context.PostAsync(inst[data_count]);
                // data[count] = activity.Text + "";
            }
            else if (data_count < 2) {
                // await context.PostAsync($"You sent {activity.Text} which was {length} characters");
                
                await context.PostAsync(inst[data_count]);
                String s = activity.Text;
                data[inst_count] = s + "";
                inst_count++;
            }
            data_count++;
            if  (data_count == 3){
                int rand = rnd.Next(4); // creates a number between 0 and 3
                await context.PostAsync("Hi " + data[0] + " you have a very bright future you should " + future[rand] + ".");
                data = new string[2] { "", "" };
                data_count = 0;
                inst_count = 0;
                await context.PostAsync("Lets move on to someone else now.");
            }
            

            context.Wait(MessageReceivedAsync);
        }
    }
}