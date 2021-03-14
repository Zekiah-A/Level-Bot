using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Level_Bot
{
    class Program
    {
        public static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

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
            int lengthOfCommand = -1;

            //checking messages for commands begin here
            if (!message.Content.StartsWith('!')) //This is your prefix
                return Task.CompletedTask;

            if (message.Author.IsBot) //ei bot commands :D
                return Task.CompletedTask;
            else
                lengthOfCommand = message.Content.Length;

            command = message.Content.Substring(1, lengthOfCommand - 1).ToLower();

            //Commands begin here
            #region COMMANDS
            if (command.Equals("hello")) //rm
            {
                message.Channel.SendMessageAsync($@"Hello {message.Author.Mention}");
            }
            else if (command.Equals("age")) //rm
            {
                message.Channel.SendMessageAsync($@"Your account was created at {message.Author.CreatedAt.DateTime.Date}");
            }
            else if(command.Equals("help")){
                Help(message);
            }
            else if(command.Equals("help commands"))
            {
                HelpCommands(message);
            }
            else if(command.Equals("help levels"))
            {
                HelpLevels(message);
            }
            else if(command.Equals("help moderation"))
            {
                HelpModeration(message);
            }
            else
            {
                if(command.Length > 1)
                {
                    message.Channel.SendMessageAsync("Invalid Command: " + command);
                }
            }
            /*
            //STUPID BIT :D
            else if(command.Equals("isjmochiicool"))
            {
                message.Channel.SendMessageAsync("Indeed, it seems that is he quite cool, possibly sublime???? \n :sunglasses:");
            }
            else if(command.Equals("isghastcool"))
            {
                message.Channel.SendMessageAsync("**TRUMP, TRUMP, TRUMP** \n :rainbow_flag:");
            }
            else if(command.Equals("iszekiahepiccool"))
            {
                message.Channel.SendMessageAsync(":sunglasses: \n He is my maker... He is the pinnacle of cool!");
            }
            else if(command.Equals("ischqrrycool"))
            {
                message.Channel.SendMessageAsync("!isjmochiicool");
            }
            else if(command.Equals("ispettericool"))
            {
                message.Channel.SendMessageAsync("__se on mita se on...__");
            }
            else if(command.Equals("ismihicool"))
            {
                message.Channel.SendMessageAsync("**ROCKET LEAGUE**... Yeah, he is vey cool, but that rocket league game is quite hard. :joy: \n `according to ghast:` \n `its not hard` \n `its ez to play` \n `hard to get good` \n Yeah... whatever lol.");
            }
            //END OF STUPID BIT
            */
            #endregion
            return Task.CompletedTask;
        }
        
        #region HELP COMMANDS

        private void Help(SocketMessage message)
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("SCPE Vote bot help:");
            builder.AddField("Commands", "!help comamnds", true);    // true - for inline
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
            builder.AddField("!vote", "Vote for the server :slight_smile:.", true);

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
    }
}
