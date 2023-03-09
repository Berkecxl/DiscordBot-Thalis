using Discord_Bot.Abstract;
using Discord_Bot.Helper.BaseHelper;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    public class CasualCommands : BaseCommandModule
    {
        #region Inject Methods
        public CasualCommands()
        {
            InjectUserHelper(new UserHelper());
        }

        IUserHelper _userHelper;

        public void InjectUserHelper(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }
        #endregion

        [Command("bilgi")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task PersonalInfo(CommandContext ctx)
        {
            var userId = ctx.User.Id.ToString();
            var userCreateDate = ctx.User.CreationTimestamp.DateTime.ToString();
            var userJoinedDate = ctx.Member.JoinedAt.DateTime.ToString();
            
            var message = new DiscordMessageBuilder()
            .AddEmbed(new DiscordEmbedBuilder()
            .AddField("Kullanıcı Adı", ctx.User.Username+"#"+ctx.User.Discriminator, true)
            .AddField("Kullanıcı ID'si", userId, true)
            .AddField("Ceza Puanı", "0",true)
            .AddField("Hesap Kurulma Tarihi", userCreateDate, true)
            .AddField("Sunucuya Katılma Tarihi", userJoinedDate, true)
            .WithColor(DiscordColor.PhthaloBlue)
            );

            await ctx.Channel.SendMessageAsync(message);

        }

        [Command("yardım")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task HelpCommand(CommandContext ctx)
        {
            var casualButton = new DiscordButtonComponent(ButtonStyle.Success, "casualButton", "Genel");
            var funButton = new DiscordButtonComponent(ButtonStyle.Success, "funButton", "Eğlence");
            var gameButton = new DiscordButtonComponent(ButtonStyle.Success, "gameButton", "Oyun");
            

            var helpMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()

                .WithColor(DiscordColor.Azure)
                .WithTitle("Yardım Menüsü")
                .WithDescription("Yardım almak istediğiniz komut bölümünü seçiniz.")
                )
                .AddComponents(casualButton, funButton, gameButton);

            await ctx.Channel.SendMessageAsync(helpMessage);
        }
    }
}
