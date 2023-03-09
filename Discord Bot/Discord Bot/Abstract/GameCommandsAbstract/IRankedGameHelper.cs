using Discord_Bot.Helper;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Abstract
{
    interface IRankedGameHelper
    {
        DiscordColor SetColor(string rank);
        DiscordEmbedBuilder UserRankMessage(RankedGameHelper rank, DiscordColor color);
        DiscordEmbedBuilder RankedWinner(RankedGameHelper userRank, RankedGameHelper botRank);
    }
}
