using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Discord_Bot.Modules
{
    public class mass : ModuleBase<Discord.Commands.SocketCommandContext>
    {
        [Command("mass")]
        public async Task massReq()
        {

            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Verified aaMember");

            if (user.Roles.Contains(role)){
                if (Program.stackCooldownTarget.Contains(Context.User as SocketGuildUser))
                {
                    //If they have used this command before, take the time the user last did something, add 5 seconds, and see if it's greater than this very moment.
                    if (Context.Message.Channel.Name == "generate")
                    {
                        if (Program.stackCooldownTimer[Program.stackCooldownTarget.IndexOf(Context.Message.Author as SocketGuildUser)].AddSeconds(400) >= DateTimeOffset.Now)
                        {
                            if (Context.Message.Channel.Name == "generate")
                            {
                                //If enough time hasn't passed, reply letting them know how much longer they need to wait, and end the code.
                                int secondsLeft = (int)(Program.stackCooldownTimer[Program.stackCooldownTarget.IndexOf(Context.Message.Author as SocketGuildUser)].AddSeconds(240) - DateTimeOffset.Now).TotalSeconds;
                                await ReplyAsync($"Hey! You have to wait at least {secondsLeft} seconds before you can use that command again!");
                                Thread.Sleep(1000);
                                int aa = 100;
                                var messagess = await this.Context.Channel.GetMessagesAsync(aa + 1).Flatten();


                                await this.Context.Channel.DeleteMessagesAsync(messagess);
                                await this.Context.Channel.SendMessageAsync("To generate a steam account please type **o.steam**. " + System.Environment.NewLine +
                    "Once you do this you will be private messaged by Vex and the message will contain the username and password for the account generated." + System.Environment.NewLine +
                    "(*Example: Username:Password*)");
                                return;
                            }

                        }
                        else
                        {
                            //If enough time has passed, set the time for the user to right now.
                            Program.stackCooldownTimer[Program.stackCooldownTarget.IndexOf(Context.Message.Author as SocketGuildUser)] = DateTimeOffset.Now;
                        }
                    }
                    else
                    {
                        //If they've never used this command before, add their username and when they just used this command.
                        Program.stackCooldownTarget.Add(Context.User as SocketGuildUser);
                        Program.stackCooldownTimer.Add(DateTimeOffset.Now);
                    }
                }

                if (Context.Message.Channel.Name == "generate")
                {
                    var lines = File.ReadAllLines(@"accounts.txt");
                    var r = new Random();
                    var randomLineNumber = r.Next(0, lines.Length - 1);
                    var line = lines[randomLineNumber];

                    var eb = new EmbedBuilder();
                    eb.WithTitle("Account Generator");
                    eb.WithColor(Discord.Color.Blue);
                    eb.AddInlineField("3 Accounts has been messaged to ", Context.User.Mention);
                    await Context.Channel.SendMessageAsync("", false, eb);


                    var u = Context.Message.Author;
                    eb = new EmbedBuilder();
                    eb.WithTitle("Account Generator");
                    eb.WithColor(Discord.Color.Blue);

                    //eb = new EmbedBuilder();
                    //eb.WithTitle("Account Generator");
                    //eb.WithColor(Discord.Color.Blue);
                    //eb.AddInlineField("Username:", Regex.Split(lines[0], ":")[0]);
                    //eb.AddInlineField("Password:", Regex.Split(lines[0], ":")[1]);
                    //await UserExtensions.SendMessageAsync(u, "", false, eb);

                    //eb = new EmbedBuilder();
                    //eb.WithTitle("Account Generator");
                    //eb.WithColor(Discord.Color.Blue);
                    //eb.AddInlineField("Username:", Regex.Split(lines[0], ":")[2]);
                    //eb.AddInlineField("Password:", Regex.Split(lines[0], ":")[3]);
                    //await UserExtensions.SendMessageAsync(u, "", false, eb);

                    //eb = new EmbedBuilder();
                    //eb.WithTitle("Account Generator");
                    //eb.WithColor(Discord.Color.Blue);
                    //eb.AddInlineField("Username:", Regex.Split(lines[0], ":")[4]);
                    //eb.AddInlineField("Password:", Regex.Split(lines[0], ":")[5]);
                    //await UserExtensions.SendMessageAsync(u, "", false, eb);

                    await UserExtensions.SendMessageAsync(u, "Account Generated: " + line);
                    await UserExtensions.SendMessageAsync(u, "Account Generated: " + line);
                    await UserExtensions.SendMessageAsync(u, "Account Generated: " + line);

                    Console.WriteLine("Account Generated: " + line);
                    Console.WriteLine("Generated For: " + Context.User.Id + " " + Context.User.Username);
                    Console.WriteLine(Context.Message.CreatedAt);
                    Thread.Sleep(1000);
                    int a = 100;
                    var messages = await this.Context.Channel.GetMessagesAsync(a + 1).Flatten();
                    await this.Context.Channel.DeleteMessagesAsync(messages);
                    await this.Context.Channel.SendMessageAsync("To generate a steam account please type **o.steam**. " + System.Environment.NewLine +
        "Once you do this you will be private messaged by Vex and the message will contain the username and password for the account generated." + System.Environment.NewLine +
        "(*Example: Username:Password*)");
                }


            }
            else
            {
                int a = 100;
                var messages = await this.Context.Channel.GetMessagesAsync(a + 1).Flatten();
                await this.Context.Channel.DeleteMessagesAsync(messages);
                await this.Context.Channel.SendMessageAsync("To generate a steam account please type **o.steam**. " + System.Environment.NewLine +
"Once you do this you will be private messaged by Vex and the message will contain the username and password for the account generated." + System.Environment.NewLine +
"(*Example: Username:Password*)");
            }
        }
    }

}
