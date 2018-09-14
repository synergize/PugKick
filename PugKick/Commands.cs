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

        [Command("roles")]
        public async Task Roles()
        {
            List<ulong> roles = new List<ulong>();
            List<ulong> memberIDs = new List<ulong>();


            var rolesFromGuild = Context.Guild.Roles.ToList();
            var gUsers = Context.Guild.Users.ToList();
            for (int i = 0; i < gUsers.Count; i++)
            {
                await ReplyAsync($"{gUsers[i]}");
            }
        }

            //for (int i = 0; i < Context.Guild.Roles.Count; i++)
            //{
            //    roles.Add(rolesFromGuild[i]);
            //    memberIDs.Add(gUsers[i]);
               
            //}        }

        [Command("userinfo"), Summary("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
            await ReplyAsync($"{userInfo.Id}");

            for (int i = 0; i < Context.Guild.Users.Count; i++)
            {
                
            }
        }
    }


    


    
}
