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
    public class ExampleModule
    {
        private DiscordSocketClient _client;

        public ExampleModule(DiscordSocketClient client)
        {
            _client = client;
        }
        
        [Command("say"), Alias("s")]
        [Remarks("Make the bot say something")]
        [MinPermissions(AccessLevel.ServerAdmin)]
        public async Task Say(IUserMessage msg, [Remainder]string text)
        {
            await msg.Channel.SendMessageAsync(text);
        }

        [Module("set"), Name("Example")]
        public class Set
        {
            private DiscordSocketClient _client;

            public Set(DiscordSocketClient client)
            {
                _client = client;
            }

            [Command("nick")]
            [Remarks("Make the bot say something")]
            [MinPermissions(AccessLevel.User)]
            public async Task Nick(IUserMessage msg, [Remainder]string name)
            {
                var user = msg.Author as IGuildUser;
                await user.ModifyAsync(x => x.Nickname = name);

                await msg.Channel.SendMessageAsync($"{user.Mention} I changed your name to **{name}**");
            }

            [Command("botnick")]
            [Remarks("Make the bot say something")]
            [MinPermissions(AccessLevel.ServerOwner)]
            public async Task BotNick(IUserMessage msg, [Remainder]string name)
            {
                var guild = (msg.Channel as IGuildChannel)?.Guild;
                var self = await guild.GetCurrentUserAsync();
                await self.ModifyAsync(x => x.Nickname = name);

                await msg.Channel.SendMessageAsync($"I changed my name to **{name}**");
            }
        }
    }
}
