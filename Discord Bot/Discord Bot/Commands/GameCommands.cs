using Discord_Bot.Abstract;
using Discord_Bot.Helper;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    public class GameCommands : BaseCommandModule
    {
        #region Inject Methods     
        public GameCommands()
        {
            InjectHelper(new RankedGameHelper());
        }

        IRankedGameHelper _gameCommandsHelper;

        private void InjectHelper(IRankedGameHelper rankBuilder) 
        {
            this._gameCommandsHelper = rankBuilder;
        }
        #endregion

        [Command("ranked")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task RankGame(CommandContext ctx) 
        {
            var userRank = new RankedGameHelper();

            var userColor = _gameCommandsHelper.SetColor(userRank.SelectedRankName);
            var userRankMessage = _gameCommandsHelper.UserRankMessage(userRank, userColor);

            await ctx.Channel.SendMessageAsync(userRankMessage);

            var botRank = new RankedGameHelper();

            var botColor = _gameCommandsHelper.SetColor(botRank.SelectedRankName);

            var botRankMessage = new DiscordEmbedBuilder()
            {
                Color = botColor,
                Title = "Bot",
                Description = "Rakibin Rank " + botRank.SelectedRank,
            };

            await ctx.Channel.SendMessageAsync(botRankMessage);

            var winner = _gameCommandsHelper.RankedWinner(userRank, botRank);
            
            await ctx.Channel.SendMessageAsync(winner);

        }

        [Command("kılıçkalkan")]
        public async Task GayWar(CommandContext ctx , string name) 
        { 
         
                 
        
        
        }
    }
}
