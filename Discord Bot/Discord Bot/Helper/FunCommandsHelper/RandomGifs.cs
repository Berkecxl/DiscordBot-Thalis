using Discord_Bot.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Helper
{
    public class RandomGifs : IRandomGifs
    {
        public string[] LoveGifs = 
        { "https://i.kym-cdn.com/photos/images/newsfeed/000/800/536/4a0.gif",
          "https://animemotivation.com/wp-content/uploads/2021/04/sao-asuna-and-kirito-scene-gif.gif",
          "http://kindyou.com/wp-content/uploads/2018/10/Super-Cute-Anime-Love-Gifs-7.gif",
          "https://media.tenor.com/HRKval6N9ksAAAAC/akame-ga-kill-esdeath.gif",
          "https://media.tenor.com/PJdw6qCLD1MAAAAC/issei-akeno.gif",
          "https://media4.giphy.com/media/l2QDM9Jnim1YVILXa/giphy.gif",
          "https://64.media.tumblr.com/42b200ad43fe46c45550e4aa42a5d4b0/tumblr_op36fw26AI1r48mnao1_500.gif",
          "https://media.tenor.com/HPuXnnAhZZcAAAAC/nanatsu-no.gif",
        };

        public string[] PunchGifs =
        {
            "https://media1.giphy.com/media/NuiEoMDbstN0J2KAiH/giphy.gif",
            "https://i.pinimg.com/originals/f3/ec/8c/f3ec8c256cb22279c14bfdc48c92e5ab.gif",
            "https://i.pinimg.com/originals/e1/63/ff/e163ff743644a8250d4f07112b8ddb08.gif",
            "https://media.tenor.com/qDDsivB4UEkAAAAC/anime-fight.gif",
            "https://3.bp.blogspot.com/-f2C5CBKw05A/W95nlOPZ4HI/AAAAAAABXVo/eU16NRt_qQIh64c3AvSScDYuRL2H6lAegCKgBGAs/s1600/Omake%2BGif%2BAnime%2B-%2BFairy%2BTail%2BFinal%2BSeason%2B-%2BEpisode%2B282%2B-%2BLucy%2BPunch.gif",
            "https://aniyuki.com/wp-content/uploads/2022/07/aniyuki-anime-girl-in-fight-32.gif",
            "https://gifimage.net/wp-content/uploads/2018/05/saitama-punch-gif-4.gif",
            "https://i.pinimg.com/originals/66/76/7a/66767af902113b20978f5880593b29af.gif",
            "https://64.media.tumblr.com/2b804d751d0552260ba533293bb78982/tumblr_mtceizZJQt1soxjwco1_1280.gif"
        };
        public string SelectedGif { get; internal set; }

        public string GetLoveRandomGif() 
        {
            var random = new Random();
            int indexGif = random.Next(0, this.LoveGifs.Length);

            this.SelectedGif = this.LoveGifs.ElementAt(indexGif);
            return SelectedGif;
        }

        public string GetPunchRandomGif() 
        {
            var random = new Random();
            int indexGif = random.Next(0, this.PunchGifs.Length);

            this.SelectedGif = this.PunchGifs.ElementAt(indexGif);
            return SelectedGif;
        }
    }
}
