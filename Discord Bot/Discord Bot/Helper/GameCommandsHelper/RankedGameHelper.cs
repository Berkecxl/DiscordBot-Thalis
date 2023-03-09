using Discord_Bot.Abstract;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Helper
{
    public class RankedGameHelper : IRankedGameHelper
    {
        public int[] RankNumbers = { 1, 2, 3 };
        public string[] RankNames = { "Demir", "Bronz", "Gümüş", "Altın" , "Platin" , "Elmas" , "Yücelik" , "Immortal"};

        public int SelectedNumber { get; internal set; }
        public string SelectedRank{ get; internal set; }
        public string SelectedRankName { get; internal set; }

        public RankedGameHelper()
        {
            var random = new Random();
            int indexNumbers = random.Next(0, this.RankNumbers.Length);
            int indexRank = random.Next(0, this.RankNames.Length);

            this.SelectedNumber = this.RankNumbers.ElementAt(indexNumbers);
            this.SelectedRankName = this.RankNames.ElementAt(indexRank);
            this.SelectedRank = this.RankNames.ElementAt(indexRank) + (" ") + this.RankNumbers.ElementAt(indexNumbers);
        }

        public DiscordColor SetColor(string rank) 
        {
            if (rank == "Demir")
                return DiscordColor.DarkGray;
            else if (rank == "Bronz")
                return DiscordColor.Brown;
            else if (rank == "Gümüş")
                return DiscordColor.Gray;
            else if (rank == "Altın")
                return DiscordColor.Gold;
            else if (rank == "Platin")
                return DiscordColor.PhthaloGreen;
            else if (rank == "Elmas")
                return DiscordColor.HotPink;
            else if (rank == "Yücelik" )
                return DiscordColor.SpringGreen;
            else if (rank == "Immortal")
                return DiscordColor.DarkRed;
            else
                return DiscordColor.White;
        }

        public DiscordEmbedBuilder UserRankMessage(RankedGameHelper rank, DiscordColor color) 
        {
            var userRankMessage = new DiscordEmbedBuilder()
            {
                Color = color,
                Title = "Oyuncu",
                Description = "Kanka Senin Rank " + rank.SelectedRank,
            };

            return userRankMessage;
        }

        public DiscordEmbedBuilder RankedWinner(RankedGameHelper userRank, RankedGameHelper botRank) 
        {
            int userPow = Array.IndexOf(RankNames,userRank.SelectedRankName);

            int botPow = Array.IndexOf(RankNames, botRank.SelectedRankName);
            if (userPow > botPow)
            {
                var userWinMessage = new DiscordEmbedBuilder()
                {
                    Color = DiscordColor.Green,
                    Title = "**KAZANDIN!!**",
                    Description = "ELONDA 500 KILL KANKAĞĞĞĞĞ",
                    ImageUrl = "https://media.tenor.com/vHDKIdICV_AAAAAM/volibear-lol.gif"
                };
                return userWinMessage;
            }
            else if (botPow > userPow)
            {
                var botWinMessage = new DiscordEmbedBuilder()
                {
                    Color = DiscordColor.Red,
                    Title = "**KAYBETTİN!!**",
                    Description = "bEZ, gEZ, kEZ, tEZ enEZ, obEZ, üvEZ çepEZ, çerEZ, çömEZ, diyEZ, eğrEZ, falEZ, firEZ, güvEZ, kepEZ, melEZ, ortEZ, ölmEZ, pünEZ ançüEZ, çerkEZ, erimEZ, fernEZ, geçmEZ, görmEZ, körfEZ, menfEZ, merkEZ, müfrEZ, pekmEZ, protEZ sentEZ, trapEZ anamnEZ, antitEZ, çekelEZ, çekemEZ, etyemEZ, göbelEZ, hipotEZ, mayonEZ, metatEZ, muazzEZ, polonEZ balyemEZ, benzemEZ, bilinmEZ, bölünmEZ, değişmEZ epigenEZ, görünmEZ hüryemEZ, kerkenEZ, manganEZ, mücehhEZ, parantEZ, tükenmEZ, varyemEZ, beklenmEZ, bilinemEZ, filogenEZ, güngörmEZ, haletinEZ, lebdeğmEZ, ontogenEZ, renksemEZ fotosentEZ, hıdırellEZ, interkinEZ, karyokinEZ, sugötürmEZ değerbilmEZ, dudakdeğmEZ, kadirbilmEZ, kargasekmEZ, sözgötürmEZ antrparantEZ, iyilikbilmEZ, karıncaezmEZ partenogenEZ, sözünübilmEZ kurşungeçirmEZ, karıncaincitmEZ.",
                    ImageUrl = "https://media.tenor.com/bdXrzy-_-LMAAAAd/sage-dance-funny-dance.gif"
                };
                return botWinMessage;
            }
            else 
            {
                if (userRank.SelectedNumber > botRank.SelectedNumber)
                {
                    var userWinMessage = new DiscordEmbedBuilder()
                    {
                        Color = DiscordColor.Green,
                        Title = "**KAZANDIN!!**",
                        Description = "ELONDA 500 KILL KANKAĞĞĞĞĞ",
                        ImageUrl = "https://media.tenor.com/vHDKIdICV_AAAAAM/volibear-lol.gif"
                    };
                    return userWinMessage;
                }
                else if (userRank.SelectedNumber < botRank.SelectedNumber)
                {
                    var botWinMessage = new DiscordEmbedBuilder()
                    {
                        Color = DiscordColor.Red,
                        Title = "**KAYBETTİN!!**",
                        Description = "bEZ, gEZ, kEZ, tEZ enEZ, obEZ, üvEZ çepEZ, çerEZ, çömEZ, diyEZ, eğrEZ, falEZ, firEZ, güvEZ, kepEZ, melEZ, ortEZ, ölmEZ, pünEZ ançüEZ, çerkEZ, erimEZ, fernEZ, geçmEZ, görmEZ, körfEZ, menfEZ, merkEZ, müfrEZ, pekmEZ, protEZ sentEZ, trapEZ anamnEZ, antitEZ, çekelEZ, çekemEZ, etyemEZ, göbelEZ, hipotEZ, mayonEZ, metatEZ, muazzEZ, polonEZ balyemEZ, benzemEZ, bilinmEZ, bölünmEZ, değişmEZ epigenEZ, görünmEZ hüryemEZ, kerkenEZ, manganEZ, mücehhEZ, parantEZ, tükenmEZ, varyemEZ, beklenmEZ, bilinemEZ, filogenEZ, güngörmEZ, haletinEZ, lebdeğmEZ, ontogenEZ, renksemEZ fotosentEZ, hıdırellEZ, interkinEZ, karyokinEZ, sugötürmEZ değerbilmEZ, dudakdeğmEZ, kadirbilmEZ, kargasekmEZ, sözgötürmEZ antrparantEZ, iyilikbilmEZ, karıncaezmEZ partenogenEZ, sözünübilmEZ kurşungeçirmEZ, karıncaincitmEZ.",
                        ImageUrl = "https://media.tenor.com/bdXrzy-_-LMAAAAd/sage-dance-funny-dance.gif"
                    };
                    return botWinMessage;
                }
                else 
                {
                    var drawMessage = new DiscordEmbedBuilder()
                    {
                        Color = DiscordColor.White,
                        Title = "**BERABERE!!**",
                        Description = "ENTİ KANKAĞ ENTİ",
                        ImageUrl = "https://media.tenor.com/SMW2b2THDiIAAAAd/valorant-cypher.gif"
                    };
                    return drawMessage;

                }
            }
        }
    }
}
