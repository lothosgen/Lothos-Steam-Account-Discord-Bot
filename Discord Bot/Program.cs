using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.Net;
using Discord.Rest;
using Discord.Rpc;
using Discord.Webhook;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Discord_Bot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public static List<DateTimeOffset> stackCooldownTimer = new List<DateTimeOffset>();
        public static List<SocketGuildUser> stackCooldownTarget = new List<SocketGuildUser>();

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "NDY4OTQ1OTc5OTM3MTI4NDQ5.DwcLfA.NtUTkTSTqFc1VecU57POCIrw03c";

            // event subscriptions
            _client.Log += Log;
            _client.UserJoined += JoinAnnounce;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task JoinAnnounce(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome, {user.Mention} to {channel.Name}!");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;


            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;
            if(message == message && message.Channel.Name == "generate" && !message.HasStringPrefix("o.", ref argPos))
            {
                if (message.IsPinned)
                {
                    message = message;
                }
                else
                {
                    Thread.Sleep(1000);
                    int aa = 100;
                    var messagess = await message.Channel.GetMessagesAsync(aa + 1).Flatten();

                    await message.Channel.DeleteMessagesAsync(messagess);
                    await message.Channel.SendMessageAsync("To generate a steam account please type **o.steam**. " + System.Environment.NewLine +
        "Once you do this you will be private messaged by Vex and the message will contain the username and password for the account generated." + System.Environment.NewLine +
        "(*Example: Username:Password*)");
                }

            }
            if (message.HasStringPrefix("o.", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                await _client.SetGameAsync("Type o.steam in #generate");
                
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
            
        }
    }
}
