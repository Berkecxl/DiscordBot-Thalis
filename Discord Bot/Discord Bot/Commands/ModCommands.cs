using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using System.Runtime.InteropServices;
using DSharpPlus.Exceptions;
using DSharpPlus.CommandsNext.Exceptions;
using Discord_Bot.Constant;

namespace Discord_Bot.Commands
{
    public class ModCommands : BaseCommandModule
    {

        [Command("sil")]
        [Cooldown(1, 3, CooldownBucketType.Guild)]
        public async Task DeleteMessage(CommandContext ctx, int piece)
        {
            if (!(ctx.Member.Permissions.HasPermission(Permissions.ManageMessages)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }

            if (piece > 51 || piece < 0)
            {
                await ctx.Channel.SendMessageAsync("Lütfen 0 ile 50 arasında bir sayı seçiniz.");
            }
            else
            {
                //ctx.Channel.DeleteMessagesAsync(new DiscordMessage());
            }

        }

        //etiketli ban
        [Command("ban")]
        [Cooldown(2, 86400, CooldownBucketType.User)]
        public async Task TagBanMember(CommandContext ctx, DiscordMember user, params string[] reason)
        {

            if (!(ctx.Member.Permissions.HasPermission(Permissions.ManageGuild)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }


            try
            {
                var isUserBannedAlready = ctx.Guild.GetBanAsync(user.Id);

                if (isUserBannedAlready.Result != null)
                {
                    throw new Exception();
                }
                await ctx.Guild.BanMemberAsync(user.Id, 0, string.Join(" ", reason));
            }
            catch (Exception ex)
            {
                if (ex.InnerException is UnauthorizedException)
                {

                }
                else if (ex.InnerException is NotFoundException)
                {
                    await ctx.Channel.SendMessageAsync("Böyle bir kullanıcı bulunamadı.");
                    throw new Exception();
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("Bu kullanıcı zaten banlı.");
                    throw new Exception();
                }
            }

            var banMessage = new DiscordEmbedBuilder()
            {
                Title = "BAN",
                Description = (ctx.Member.Username + ", " + user.Username + " kullanıcısını "+"**"+reason+"**"+" sebebiyle başarıyla yasakladı."),
                ImageUrl = "https://media.tenor.com/KJgAfNXWdFEAAAAC/thumbs-down.gif"
            };
            await ctx.Channel.SendMessageAsync(embed: banMessage);

        }
        
        //id'li ban
        [Command("ban")]
        [Cooldown(2, 86400, CooldownBucketType.User)]
        public async Task IdBanMember(CommandContext ctx, ulong id, params string[] reason)
        {

            if (!(ctx.Member.Permissions.HasPermission(Permissions.ManageGuild)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }


            try
            {
                var isUserBannedAlready = ctx.Guild.GetBanAsync(id);

                if (isUserBannedAlready.Result != null)
                {
                    throw new Exception();
                }
                await ctx.Guild.BanMemberAsync(id, 0, string.Join(" ", reason));
            }
            catch (Exception ex)
            {
                if (ex.InnerException is UnauthorizedException)
                {

                }
                else if (ex.InnerException is NotFoundException)
                {
                    await ctx.Channel.SendMessageAsync("Böyle bir kullanıcı bulunamadı.");
                    throw new Exception();
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("Bu kullanıcı zaten banlı.");
                    throw new Exception();
                }
            }

            var banMessage = new DiscordEmbedBuilder()
            {
                Title = "BAN",
                Description = (ctx.Member.Username + ", " + Convert.ToString(id) + " id'li kullanıcıyı " + "**" + reason + "**" + " sebebiyle başarıyla yasakladı."),
                ImageUrl = "https://media.tenor.com/KJgAfNXWdFEAAAAC/thumbs-down.gif"
            };
            await ctx.Channel.SendMessageAsync(embed: banMessage);

        }

        [Command("unban")]
        [Cooldown(5, 120, CooldownBucketType.User)]
        public async Task UnbanMember(CommandContext ctx, ulong id)
        {
            if (!(ctx.Member.Permissions.HasPermission(Permissions.ManageGuild)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }

            try
            {
                await ctx.Guild.UnbanMemberAsync(id);
            }
            catch(Exception ex)
            {
                if (ex.InnerException is UnauthorizedException)
                {
                    
                }
                else if (ex.InnerException is NotFoundException)
                {
                    await ctx.Channel.SendMessageAsync("Böyle bir kullanıcı bulunamadı.");
                    throw new Exception();
                }
                else
                {
                    await ctx.Channel.SendMessageAsync("Bu kullanıcı zaten banlı.");
                    throw new Exception();
                }
            }

            var unbanMessage = new DiscordEmbedBuilder()
            {
                Title = "UNBAN",
                Description = (ctx.Member.Username + ", " + Convert.ToString(id) + " id'li kullanıcının yasağını başarıyla kaldırdı."),
                ImageUrl = "https://media.tenor.com/RD_Ajk295VcAAAAd/chill.gif"
            };
            await ctx.Channel.SendMessageAsync(embed: unbanMessage);
        }

        //etiketli mute, TODO Değişecek
        [Command("mute")]
        [Cooldown(5, 120, CooldownBucketType.User)]
        public async Task MuteMember(CommandContext ctx, ulong id, int time)
        {
            if (!(ctx.Member.Permissions.HasPermission(Permissions.MuteMembers)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }

            if (time > 0 && time < 600)
            {
                DateTime dateTime = DateTime.Now.AddMinutes(time);
                DiscordMember member = await ctx.Guild.GetMemberAsync(id);
                DiscordRole muteRole = ctx.Guild.GetRole(1081617273694990428);
                await member.GrantRoleAsync(muteRole);
            }

        }

        //etiketli erkek kayıt
        [Command("e")]
        [Cooldown(15, 120, CooldownBucketType.User)]
        public async Task TagSignManMember(CommandContext ctx, DiscordMember user)
        {
            DiscordRole memberRole = ctx.Guild.GetRole(Constants.MemberRoleId);

            if (ctx.Member.Roles.FirstOrDefault(x => x.Id == Constants.SignerRoleId) == null || !(ctx.Member.Permissions.HasPermission(Permissions.ChangeNickname)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }

            if (user.Roles.FirstOrDefault(x => x.Id == Constants.SignerRoleId) != null)
            {
                await ctx.Channel.SendMessageAsync("Bu kullanıcı zaten kayıtlı");
            }
            else
            {
                try
                {
                    await user.GrantRoleAsync(memberRole);
                    await ctx.Channel.SendMessageAsync("Kayıt Başarılı ile gerçekleşti.");
                }
                catch (Exception ex)
                {
                    await ctx.Channel.SendMessageAsync(ex.Message);
                }
            }
        }

        //id'li erkek kayıt
        [Command("e")]
        [Cooldown(15, 120, CooldownBucketType.User)]
        public async Task IdSignManMember(CommandContext ctx, ulong id)
        {
            DiscordRole memberRole = ctx.Guild.GetRole(Constants.MemberRoleId);
            DiscordMember user = await ctx.Guild.GetMemberAsync(id);

            if (ctx.Member.Roles.FirstOrDefault(x => x.Id == Constants.SignerRoleId) == null || !(ctx.Member.Permissions.HasPermission(Permissions.ChangeNickname)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }

            if (user.Roles.FirstOrDefault(x => x.Id == Constants.SignerRoleId) != null)
            {
                await ctx.Channel.SendMessageAsync("Bu kullanıcı zaten kayıtlı");
            }
            else
            {
                try
                {
                    await user.GrantRoleAsync(memberRole);
                    await ctx.Channel.SendMessageAsync("Kayıt Başarılı ile gerçekleşti.");
                }
                catch (Exception ex)
                {
                    await ctx.Channel.SendMessageAsync(ex.Message);
                }
            }
        }

        //etiketli kız kayıt
        [Command("k")]
        [Cooldown(15, 120, CooldownBucketType.User)]
        public async Task TagSignGirlMember(CommandContext ctx, DiscordMember user)
        {
            DiscordRole memberRole = ctx.Guild.GetRole(Constants.MemberRoleId);
            DiscordRole girlRole = ctx.Guild.GetRole(Constants.GirlRoleId);

            if (ctx.Member.Roles.FirstOrDefault(x => x.Id == Constants.SignerRoleId) == null || !(ctx.Member.Permissions.HasPermission(Permissions.ChangeNickname)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }

            if (user.Roles.FirstOrDefault(x => x.Id == Constants.SignerRoleId) != null && user.Roles.FirstOrDefault(x => x.Id == Constants.GirlRoleId) != null)
            {
                await ctx.Channel.SendMessageAsync("Bu kullanıcı zaten kayıtlı");
            }
            else
            {
                try
                {
                    await user.GrantRoleAsync(memberRole);
                    await user.GrantRoleAsync(girlRole);
                    await ctx.Channel.SendMessageAsync("Kayıt Başarılı ile gerçekleşti.");
                }
                catch (Exception ex)
                {
                    await ctx.Channel.SendMessageAsync(ex.Message);
                }
            }
        }

        //id'li kız kayıt
        [Command("k")]
        [Cooldown(15, 120, CooldownBucketType.User)]
        public async Task IdSignGirlMember(CommandContext ctx, ulong id)
        {
            DiscordRole memberRole = ctx.Guild.GetRole(Constants.MemberRoleId);
            DiscordRole girlRole = ctx.Guild.GetRole(Constants.GirlRoleId);
            DiscordMember user = await ctx.Guild.GetMemberAsync(id);

            if (ctx.Member.Roles.FirstOrDefault(x => x.Id == Constants.SignerRoleId) == null || !(ctx.Member.Permissions.HasPermission(Permissions.ChangeNickname)))
            {
                var requirePermissonMessage = "Bu komut için gerekli yetkiye sahip değilsiniz.";

                await ctx.Channel.SendMessageAsync(requirePermissonMessage);

                throw new Exception();
            }

            if (user.Roles.FirstOrDefault(x => x.Id == Constants.SignerRoleId) != null)
            {
                await ctx.Channel.SendMessageAsync("Bu kullanıcı zaten kayıtlı");
            }
            else
            {
                try
                {
                    await user.GrantRoleAsync(memberRole);
                    await user.GrantRoleAsync(girlRole);
                    await ctx.Channel.SendMessageAsync("Kayıt Başarılı ile gerçekleşti.");
                }
                catch (Exception ex)
                {
                    await ctx.Channel.SendMessageAsync(ex.Message);
                }
            }
        }
    }
}
