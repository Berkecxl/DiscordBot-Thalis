using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    public class ModCommands : BaseCommandModule
    {

        [Command("sil")]
        [RequirePermissions(DSharpPlus.Permissions.ManageMessages)]
        [Cooldown(1,3,CooldownBucketType.Guild)]
        public async Task DeleteMessage(CommandContext ctx,int piece) 
        { 
        
          if(piece > 51 || piece < 0) 
          {
             await ctx.Channel.SendMessageAsync("Lütfen 0 ile 50 arasında bir sayı seçiniz.");
          }
          else
          {
          }
        
        
        
        
        }
    }
}
