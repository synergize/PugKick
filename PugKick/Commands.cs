using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;

namespace PugKick
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Roles()
        {
            var guild = Context.Client.Guilds.FirstOrDefault(x => x.Id == Context.Guild.Id);
            var embed = new EmbedBuilder();
            embed.WithAuthor($"Mass User Kicking", Context.Guild.IconUrl);
            embed.AddField("Commands", "!kick \"Role Name\" Quotation marks are important is role has spaces in the name!", true);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
    public class Kick : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(string roleInput)
        {
            var user = Context.User as SocketGuildUser;
            var role = ((IGuildUser) user)?.Guild.Roles.FirstOrDefault(x => x.Name == roleInput);
            if (role == null)
            {
                await ReplyAsync("Please enter a valid role registered on the server.");
                return;
            }

            var listOfGuildUsers = Context.Guild.Users.ToList();
            var socketList = Context.Guild.Users.ToList();

            for (var i = 0; i < socketList.Count; i++)
            {
                var userName = listOfGuildUsers[i];
                if (userName.Roles.Contains(role)) continue;
                if (user.GuildPermissions.KickMembers)
                {
                    try
                    {
                        await userName.KickAsync();
                    }
                    catch (Exception ex)
                    {
                        await ReplyAsync(ex.Message);
                    }
                }
                else
                {
                    await ReplyAsync("User doesn't have permission to kick.");
                }
            }
        }
    }
}
