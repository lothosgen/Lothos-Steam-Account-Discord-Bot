using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Discord_Bot.Modules
{
    public class faq : ModuleBase<SocketCommandContext>
    {
        [Command("faq")]
        public async Task PingAsync()
        {


            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Onyo");

            if (user.Roles.Contains(role))
            {
                var eb = new EmbedBuilder();
                eb.WithTitle("Commands & Features");
                eb.WithColor(Color.Blue);
                eb.AddInlineField("Prefix", "o. is the Prefix for the bot");
                eb.AddInlineField("Prefix", "o.steam will generate an account and DM it to you " + Environment.NewLine + "o.ping will call you gay");
                eb.AddInlineField("Member Count", "6");
                await Context.Channel.SendMessageAsync("", false, eb);
            } else
            {
                var eb = new EmbedBuilder();
                eb.WithTitle("You don't have permission");
                eb.WithColor(Color.Blue);
                eb.WithDescription("You must have rank Onyo to do this!");
                await Context.Channel.SendMessageAsync("", false, eb);
            }
        }
    }
}
