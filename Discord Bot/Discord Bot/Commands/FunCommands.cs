using Discord_Bot.Abstract;
using Discord_Bot.Helper;
using Discord_Bot.Helper.BaseHelper;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Emzi0767;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        #region Inject Methods
        public FunCommands()
        {
            InjectRankBuilder(new RankedGameHelper());
            InjectUserHelper(new UserHelper());
            InjectFunCommandsHelper(new RandomGifs());
        }

        IRankedGameHelper _rankBuilder;
        IUserHelper _userHelper;
        IRandomGifs _funCommandsHelper;
        
        private void InjectRankBuilder(IRankedGameHelper rankBuilder) 
        { 
           this._rankBuilder = rankBuilder;
        }
        private void InjectUserHelper(IUserHelper userHelper) 
        {
            this._userHelper = userHelper;
        }
        private void InjectFunCommandsHelper(IRandomGifs funCommandsHelper) 
        {
            this._funCommandsHelper = funCommandsHelper;
        }
        #endregion

        [Command("sev")]
        [Cooldown(1, 3,CooldownBucketType.User)]
        public async Task Love(CommandContext ctx, string name)
        {
            var randomGif = new RandomGifs();

            if (!_userHelper.IsUserValid(name))
            {
                await ctx.Channel.SendMessageAsync("Lütfen Bir Kullanıcı Etiketleyiniz.");
            }
            else
            {
                var embedMessage = new DiscordEmbedBuilder()
                {
                    Description = name + ", " + ctx.User.Username + " seni çok seviyor. ^^",
                    Color = DiscordColor.HotPink,
                    ImageUrl = _funCommandsHelper.GetLoveRandomGif()
                };
                await ctx.Channel.SendMessageAsync(embed: embedMessage);
            }
        }

        [Command("döv")]
        [Cooldown(1, 3, CooldownBucketType.User)]
        public async Task Punch(CommandContext ctx, string name)
        {
            var randomGif = new RandomGifs();

            if (!_userHelper.IsUserValid(name))
            {
                await ctx.Channel.SendMessageAsync("Lütfen Bir Kullanıcı Etiketleyiniz.");
            }
            else
            {
                var embedMessage = new DiscordEmbedBuilder()
                {
                    Description = name + ", " + ctx.User.Username + "' ın GAZABINA UĞRADI",
                    Color = DiscordColor.DarkRed,
                    ImageUrl = _funCommandsHelper.GetPunchRandomGif()
                };
                await ctx.Channel.SendMessageAsync(embed: embedMessage);
            }
        }

        [Command("embed")]
        [Cooldown(1, 3, CooldownBucketType.User)]
        [RequireRoles(RoleCheckMode.MatchNames,"YT")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task Embed(CommandContext ctx) 
        {
            if (ctx.Channel.Id == 1040272487818199161 || ctx.Channel.Id == 833169743405907992)
            {
                var embedMessage = new DiscordEmbedBuilder()
                {
                    Title = "I'll be the only one at your side.",
                    Color = DiscordColor.Magenta,
                    ImageUrl = "https://i.pinimg.com/736x/98/a7/a4/98a7a429994573ca7f8634fdbfd75522.jpg"
                };

                await ctx.RespondAsync(embed: embedMessage);
            }
        }
        
        [Command("rank")]
        [Cooldown(1, 4, CooldownBucketType.User)]
        public async Task Rank(CommandContext ctx)
        {
            var userRank = new RankedGameHelper();

            var userColor = _rankBuilder.SetColor(userRank.SelectedRankName);
            var userRankMessage = _rankBuilder.UserRankMessage(userRank, userColor);

            await ctx.Channel.SendMessageAsync(userRankMessage);
        }

        [Command("pp")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ProfilePhoto(CommandContext ctx) 
        { 

            var embedMessage = new DiscordEmbedBuilder()
            {
                Title = "Profil Fotosu",
                Color = DiscordColor.Azure,
                Description = ctx.User.Username + " Profil Fotosu",
                ImageUrl = ctx.User.AvatarUrl
            };

            ctx.Channel.SendMessageAsync(embed: embedMessage);
        }

        [Command("oylama")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task Poll(CommandContext ctx, int timeLimit, string optionOne, string optionTwo, params string[] question) 
        {
            try
            {
                var interactvity = ctx.Client.GetInteractivity(); 
                TimeSpan timer = TimeSpan.FromSeconds(timeLimit);

                DiscordEmoji[] optionEmojis = { DiscordEmoji.FromName(ctx.Client, ":one:", false),
                                            DiscordEmoji.FromName(ctx.Client, ":two:", false) };

                string optionsString = optionEmojis[0] + " | " + optionOne + "\n" +
                                       optionEmojis[1] + " | " + optionTwo;

                var pollMessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle(string.Join(" ", question))
                    .WithDescription(optionsString)
                    ); 

                var putReactOn = await ctx.Channel.SendMessageAsync(pollMessage); 

                foreach (var emoji in optionEmojis)
                {
                    await putReactOn.CreateReactionAsync(emoji); 
                }

                var result = await interactvity.CollectReactionsAsync(putReactOn, timer); 

                int count1 = 0; 
                int count2 = 0;

                foreach (var emoji in result) 
                {
                    if (emoji.Emoji == optionEmojis[0])
                    {
                        count1++;
                    }
                    if (emoji.Emoji == optionEmojis[1])
                    {
                        count2++;
                    }
                }

                int totalVotes = count1 + count2;

                string resultsString = optionEmojis[0] + ": " + count1 + " Oy \n" +
                           optionEmojis[1] + ": " + count2 + " Oy \n" +
                           "Toplam Kullanan Oy: " + totalVotes; 

                var resultsMessage = new DiscordMessageBuilder()
                    .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Green)
                    .WithTitle("Oylama Sonucu!")
                    .WithDescription(resultsString)
                    );

                await ctx.Channel.SendMessageAsync(resultsMessage);          
            }
            catch (Exception ex)
            {
                var errorMsg = new DiscordEmbedBuilder()
                {
                    Title = "Bir Hata Oluştu!",
                    Description = ex.Message,
                    Color = DiscordColor.Red
                };

                await ctx.Channel.SendMessageAsync(embed: errorMsg);
            }
        }

        
    }
}
