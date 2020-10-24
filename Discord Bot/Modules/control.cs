using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Discord_Bot.Modules
{
    public class control : ModuleBase<SocketCommandContext>
    {
        [Command("sendUpdate")]
        public async Task controlAsync(ulong channelID, string Message)
        {
            var chnl = Context.Guild.GetChannel(channelID) as IMessageChannel;
            var eb = new EmbedBuilder();

            eb = new EmbedBuilder();
            eb.WithTitle("Update");
            eb.WithColor(Discord.Color.Orange);
            eb.WithDescription(Message);
            await chnl.SendMessageAsync("", false, eb);






        }
    }
}
