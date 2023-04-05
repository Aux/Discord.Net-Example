using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Example.Modules
{
    public class ExampleModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("say", "Make the bot say something.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public Task Say(string text)
            => ReplyAsync(text);

        [SlashCommand("nick", "Change a user's nickname to the specified text")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        [RequireBotPermission(GuildPermission.ChangeNickname)]
        public async Task Nick(string name, SocketGuildUser user = null)
        {
            user ??= (SocketGuildUser)Context.User;
            await user.ModifyAsync(x => x.Nickname = name);
            await RespondAsync($"{user.Mention} I changed your name to **{name}**");
        }
    }
}
