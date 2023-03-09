using Discord_Bot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsycn() 
        { 
          var json = string.Empty;
          using (var fs = File.OpenRead("config.json"))
          using (var sr = new StreamReader(fs , new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

          var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration() 
            { 
               Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] {configJson.Prefix},
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<GameCommands>();
            Commands.RegisterCommands<CasualCommands>();
            Commands.RegisterCommands<ModCommands>();

            Commands.CommandErrored += OnCommandError;

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private async Task OnCommandError(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
           if(e.Exception is ChecksFailedException)
           {
                var castedException = (ChecksFailedException)e.Exception;
                string cooldownTimer = string.Empty;

                foreach(var check in castedException.FailedChecks)
                {
                    var cooldown = (CooldownAttribute)check;
                    TimeSpan timeLeft = cooldown.GetRemainingCooldown(e.Context);
                    cooldownTimer = timeLeft.ToString(@"ss");
                }
                var cooldownMessage = new DiscordEmbedBuilder
                {
                    Description = "Lütfen " + cooldownTimer + " saniye sonra tekrar deneyiniz.",
                    Color = DiscordColor.Red
                };

                await e.Context.Channel.SendMessageAsync(cooldownMessage);
            }
        }

        private Task OnClientReady(ReadyEventArgs e) 
        { 
          return Task.CompletedTask;
        }

    }
}
