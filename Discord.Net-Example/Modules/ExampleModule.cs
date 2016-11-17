using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Example.Attributes;
using Example.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Modules
{
    [Name("Example")]
    public class ExampleModule : ModuleBase
    {
        [Command("say"), Alias("s")]
        [Remarks("Make the bot say something")]
        [MinPermissions(AccessLevel.ServerAdmin)]
        public async Task Say([Remainder]string text)
        {
            await ReplyAsync(text);
        }

        [Group("set"), Name("Example")]
        public class Set : ModuleBase
        {
            [Command("nick")]
            [Remarks("Make the bot say something")]
            [MinPermissions(AccessLevel.User)]
            public async Task Nick([Remainder]string name)
            {
                var user = Context.User as SocketGuildUser;
                await user.ModifyAsync(x => x.Nickname = name);

                await ReplyAsync($"{user.Mention} I changed your name to **{name}**");
            }

            [Command("botnick")]
            [Remarks("Make the bot say something")]
            [MinPermissions(AccessLevel.ServerOwner)]
            public async Task BotNick([Remainder]string name)
            {
                var self = await Context.Guild.GetCurrentUserAsync();
                await self.ModifyAsync(x => x.Nickname = name);

                await ReplyAsync($"I changed my name to **{name}**");
            }
        }
    }
}
