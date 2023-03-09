using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsycn().GetAwaiter().GetResult();
        }
    }
}
