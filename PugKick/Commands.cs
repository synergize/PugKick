using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System.Collections.Immutable;
using System.Linq;

namespace PugKick
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("help")]
        public async Task Roles()
        {
            SocketGuild Guild = Context.Client.Guilds.Where(x => x.Id == Context.Guild.Id).FirstOrDefault();
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor($"Mass User Kicking", Context.Guild.IconUrl);
            Embed.AddField("Commands", "!kick \"Role Name\" Quotation marks are important is role has spaces in the name!", true);

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
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
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == roleInput);
            if (role == null)
            {
                await ReplyAsync("Please enter a valid role registered on the server.");
                return;
            }
            List<SocketGuildUser> SocketList = new List<SocketGuildUser>();

            var xyz = Context.Guild.Users.ToList();
            var listTest = xyz[0];
            foreach (SocketGuildUser str in Context.Guild.Users)
            {
                SocketList.Add(str);
            }
                
                for (int i = 0; i < SocketList.Count; i++)
            {
                var userName = xyz[i];
                if (!userName.Roles.Contains(role))
                {
                    // Do Stuff
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






}
