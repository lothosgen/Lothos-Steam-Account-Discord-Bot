using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;
using System.Drawing;
using Discord;
using System.Threading;
using System.Text.RegularExpressions;

namespace Discord_Bot.Modules
{
    public class Steam : ModuleBase<SocketCommandContext>
    {
        [Command("steam")]
        public async Task steamReq()
        {
            if (Context.Message.Channel.Name != "🤖generate")
            {
                int a;
            }
            else
            {
                if (Program.stackCooldownTarget.Contains(Context.User as SocketGuildUser))
                {
                    //If they have used this command before, take the time the user last did something, add 5 seconds, and see if it's greater than this very moment.
                    if (Program.stackCooldownTimer[Program.stackCooldownTarget.IndexOf(Context.Message.Author as SocketGuildUser)].AddSeconds(240) >= DateTimeOffset.Now)
                    {
                        //If enough time hasn't passed, reply letting them know how much longer they need to wait, and end the code.
                        int secondsLeft = (int)(Program.stackCooldownTimer[Program.stackCooldownTarget.IndexOf(Context.Message.Author as SocketGuildUser)].AddSeconds(240) - DateTimeOffset.Now).TotalSeconds;
                        await ReplyAsync($"Hey! You have to wait at least {secondsLeft} seconds before you can use that command again!");
                        Thread.Sleep(1000);
                        int aa = 100;
                        var messagess = await this.Context.Channel.GetMessagesAsync(aa + 1).Flatten();


                        await this.Context.Channel.DeleteMessagesAsync(messagess);
                        await this.Context.Channel.SendMessageAsync("To generate a steam account please type **o.steam** " + System.Environment.NewLine +
            "Once you do this you will be private messaged by Vex and the message will contain the username and password for the account generated." + System.Environment.NewLine +
            "(*Example: Username:Password*)");
                        return;
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
                try
                {
                    SocketUser msgUser = Context.Message.Author;
                    await sendAccount(msgUser);
                    

                    ulong chnID = 528358303303204864;
                    SocketTextChannel chn = Context.Guild.GetChannel(chnID) as SocketTextChannel;
                    await sendLogAlert(chn);
                    




                    Thread.Sleep(1000);
                    int a = 100;
                    var messages = await this.Context.Channel.GetMessagesAsync(a + 1).Flatten();
                    await this.Context.Channel.DeleteMessagesAsync(messages);
                    await this.Context.Channel.SendMessageAsync("To generate a steam account please type **o.steam** " + System.Environment.NewLine +
                    "Once you do this you will be private messaged by Vex and the message will contain the username and password for the account generated." + System.Environment.NewLine +
                    "(*Example: Username:Password*)");

                }
                catch (Exception e)
                {
                    Console.WriteLine("Command o.steam not working");
                    await UserExtensions.SendMessageAsync(Context.Message.Author, "The Command o.steam is currently down. Please use o.steamtest");

                }
            }






        }

        public async Task sendAccount(SocketUser user)
        {


            List<String> lines = new List<String>(File.ReadAllLines(@"accounts.txt"));
            String old = @"accounts_given";
            var eb = new EmbedBuilder();
            eb.WithTitle("Account Generator");
            eb.WithColor(Discord.Color.Blue);
            eb.AddInlineField("Account has been messaged to ", user.Mention);
            await Context.Channel.SendMessageAsync("", false, eb);

            eb = new EmbedBuilder();
            eb.WithTitle("Account Generator");
            eb.WithColor(Discord.Color.Blue);
            eb.AddInlineField("Username:", Regex.Split(lines[0], ":")[0]);
            eb.AddInlineField("Password:", Regex.Split(lines[0], ":")[1]);
            eb.Footer.WithText("managed by: champ & agentsix1");
            await UserExtensions.SendMessageAsync(user, "", false, eb);

            File.AppendAllText(old, lines[0] + "| Given To: " + Context.User.Username + Environment.NewLine);
            lines.RemoveAt(0);

            File.WriteAllLines(@"accounts.txt", lines);
            Console.WriteLine("----------------------");
            Console.WriteLine("Account Generated: " + lines[0]);
            Console.WriteLine("Generated For: " + Context.User.Id + " " + Context.User.Username);
            Console.WriteLine("You currently have " + lines.Count + " steam accounts remaining.");
            Console.WriteLine(Context.Message.CreatedAt);
            Console.WriteLine("----------------------");

        }

        public async Task sendLogAlert(SocketTextChannel chn)
        {
            List<String> lines = new List<String>(File.ReadAllLines(@"accounts.txt"));

            if (lines.Count < 10)
            {
                await chn.SendMessageAsync(lines.Count.ToString() + " <@160094810517012481> <@287782786771845133> Add More Accounts!");
            }
            else
            {
                await chn.SendMessageAsync(lines.Count.ToString() + " Steam Accounts Remaining " + "[" + Context.Message.Author.Username.ToString() + "]");
            }
        }
    }
}