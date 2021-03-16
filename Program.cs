using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Level_Bot
{
    class Program
    {
        //public static void Main(string[] args)
        //=> new Program().MainAsync().GetAwaiter().GetResult();// removed stupid lambads
        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult(); 
        }

        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler; //Send bot messages to cmd handler
            _client.Log += Log; //Send logs to the log function / task / whatever

            var token = File.ReadAllText("token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandHandler(SocketMessage message)
        {
            //variables
            string command = "";
            string args = "";
            int lengthOfCommand = -1;

            //checking messages for commands begin here
            if (!message.Content.StartsWith('!')) //This is your prefix
                return Task.CompletedTask;
            if (message.Author.IsBot) //ei bot commands :D
                return Task.CompletedTask;
            else
            {
                lengthOfCommand = message.Content.Length;

                if (message.Content.Contains(' ')) //handle cmd arguments
                { 
                    lengthOfCommand = message.Content.IndexOf(' ');
                    args = message.Content.Substring(message.Content.IndexOf(" ") + 1);
                }
            }
            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();

            //Commands begin here
            #region COMMANDS
            switch(command)
            {
                case "hello":
                    message.Channel.SendMessageAsync($@"Hello {message.Author.Mention}");
                    break;
                case "age":
                    message.Channel.SendMessageAsync($@"Your account was created at {message.Author.CreatedAt.DateTime.Date}");
                    break;
                /* HELP COMMANDS */
                case "help":
                    switch(args)
                    {
                        case "commands":
                            HelpCommands(message);
                            break;
                        case "levels":
                            HelpLevels(message);
                            break;
                        case "moderation":
                            HelpModeration(message);
                            break;
                        default:
                            Help(message);
                            break;
                    }
                    break;
                /* MODERATION COMMANDS */
                case "ban":
                    Ban(message, null, null);
                    break;
                case "kick":
                    Kick(message, null, null);
                    break;
                case "mute":
                    Mute(message, null, null);
                    break;
                /* GENERAL COMMANDS */
                case "ip":
                    Ip(message);
                    break;
                case "apply":
                    Apply(message);
                    break;
                case "vote":
                    Vote(message);
                    break;
                /* STUPID STUFF */
                case "help other":
                    message.Channel.SendMessageAsync("**`PSST, you entered the super secret panel!` \n !hello \n !age \n !isstarlkcool \n !iszekiahepiccool \n !isjmochiicool \n !amicool \n !isghastcool \n !ischqrrycool \n !ismihicool \n !ispettericool \n ||!vittu||");
                    break;
                case "isstarlkcool":
                    message.Channel.SendMessageAsync("*python, c#, v, python, c#, v, pytjon, c#, v, pyth...* \n Yes, he is very cool ||until he asks  what a struct is :joy:||.");
                    break;
                case "iszekiahepiccool":
                    message.Channel.SendMessageAsync(":sunglasses: \n He is my maker... He is the pinnacle of cool!");
                    break;
                case "isjmochiicool":
                    message.Channel.SendMessageAsync("Indeed, it seems that is he quite cool, possibly sublime???? \n :sunglasses:");
                    break;
                case "amicool":
                    message.Channel.SendMessageAsync("**A---- shush**, conceit is the bloat of society.");
                    break;
                case "isghastcool":
                    message.Channel.SendMessageAsync("**TRUMP, TRUMP, TRUMP** \n :rainbow_flag:");
                    break;
                case "ischqrrycool":
                    message.Channel.SendMessageAsync("!isjmochiicool");
                    break;
                case "ismihicool":
                    message.Channel.SendMessageAsync("**ROCKET LEAGUE**... Yeah, he is vey cool, but that rocket league game is quite hard. :joy: \n `according to ghast:` \n `its not hard` \n `its ez to play` \n `hard to get good` \n Yeah... whatever lol.");
                    break;
                case "ispettericool":
                    message.Channel.SendMessageAsync("__se on mita se on...__");
                    break;
                case "vittu":
                    message.Channel.SendFileAsync("mp4.mp4","When ei wiikend games.");
                    break;
                /* END OF STUPID */
                default:
                    if(command.Length > 1)
                    {
                        message.Channel.SendMessageAsync("Invalid Command: " + command);
                    }
                    break;
            }
            #endregion

            return Task.CompletedTask;
        }
        
        //private Task SearchAndDestroy(SocketMessage message)
        //{ } 

        #region GENERAL_COMMANDS
        private void Ip(SocketMessage message) 
        {
            message.Channel.SendMessageAsync("ip: `suomicraftpe.ddns.net`\nport: `13132`");
        }
        private void Apply(SocketMessage message)
        {
            message.Channel.SendMessageAsync("Want to apply for builder or staff? \nVisit `suomicraftpe.tk/apply` and submit an application!");
        }
        private void Vote(SocketMessage message)
        {
            message.Channel.SendMessageAsync("Thank you! You are breathtaking: `bit.ly/suomicraftpe-vote` :heartpulse:");
        }
        #endregion

        #region HELP_COMMANDS
        private void Help(SocketMessage message)
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("SCPE Vote bot help:");
            builder.AddField("Commands", "!help commands", true);    // true - for inline
            builder.AddField("Levels", "!help levels", true);
            builder.AddField("Moderation", "!help moderation", true);
            //builder.WithThumbnailUrl("http://...");

            builder.WithColor(Color.Gold);
            message.Channel.SendMessageAsync("", false, builder.Build());
        }
        private void HelpCommands(SocketMessage  message)
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Commands:");
            builder.AddField("!apply", "Apply for staff.", true);
            builder.AddField("!ip", "Show server IP.", true);
            builder.AddField("!vote", "Vote for the server. :heartpulse:", true);

            builder.WithColor(Color.Gold);
            message.Channel.SendMessageAsync("", false, builder.Build());
        }
        private void HelpLevels(SocketMessage message)
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Levels:");
            builder.AddField("!rank", "Clears messages in a channel.", true);    // true - for inline
            builder.AddField("!leaderboard / !levels", "Shows the rank leaderboard.", true);

            builder.WithColor(Color.Gold);
            message.Channel.SendMessageAsync("", false, builder.Build());
        }
        private void HelpModeration(SocketMessage message)
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Moderation:");
            builder.AddField("!clear", "Clears messages in a channel.", true);    // true - for inline
            builder.AddField("!kick", "Kicks a user from the server.", true);
            builder.AddField("!ban", "Bans a user from the server", true);
            builder.AddField("!mute", "Mutes a user for a specified time.", true);
            builder.AddField("*", "Commands accept both user ID and @mention", true);

            builder.WithColor(Color.Gold);
            message.Channel.SendMessageAsync("", false, builder.Build());
        }
        #endregion

        #region MODERATOR_COMMANDS
        private void Ban(SocketMessage message, string uuid, string reason)
        {
            string actor = message.Author.ToString();
            Console.WriteLine(actor + " banned user for reason: [null]");
        }
        private void Kick(SocketMessage message, string uuid, string reason)
        {

        }
        private void Mute(SocketMessage message, string uuid, string reason)
        {

        }
        #endregion
    }
}