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
    [Module, Name("Example")]
    [RequireContext(ContextType.Guild)]
    public class ModeratorModule
    {
        private DiscordSocketClient _client;

        public ModeratorModule(DiscordSocketClient client)
        {
            _client = client;
        }

        [Command("say"), Alias("s")]
        [Remarks("Make the bot say something")]
        [MinPermissions(AccessLevel.ServerMod)]
        public async Task Say(IUserMessage msg, [Remainder]IUser user)
        {
            var u = user as IGuildUser;
            await msg.Channel.SendMessageAsync($"cya {u.Mention} :wave:");
            await u.KickAsync();
        }
    }
}
