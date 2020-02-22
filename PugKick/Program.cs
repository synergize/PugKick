using System;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Collections.Generic;

namespace PugKick
{
    class Program
    {
            private DiscordSocketClient Client;
        private CommandService Commands;
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug

            });

            Client.MessageReceived += Client_MessageRecieved;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());
            Client.Ready += Client_Ready;
            Client.Log += Client_Log;

            string Token = System.Environment.GetEnvironmentVariable("PUG_TOKEN");
            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_MessageRecieved(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasCharPrefix('!', ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");




        }
        private async Task Client_Ready()
        {
            await Client.SetGameAsync("*snap*", "https://discordapp.com/developers", StreamType.NotStreaming);
        }
    }
    
}
