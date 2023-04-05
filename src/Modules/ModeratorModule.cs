using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Example.Modules
{
    public class ModeratorModule : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("kick", "Kick the specified user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser user)
        {
            await ReplyAsync($"cya {user.Mention} :wave:");
            await user.KickAsync();
        }
    }
}
