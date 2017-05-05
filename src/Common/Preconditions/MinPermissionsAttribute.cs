using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Preconditions
{
    /// <summary>
    /// Set the minimum permission required to use a module or command
    /// similar to how MinPermissions works in Discord.Net 0.9.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MinPermissionsAttribute : PreconditionAttribute
    {
        private AccessLevel Level;

        public MinPermissionsAttribute(AccessLevel level)
        {
            Level = level;
        }

        public override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var access = GetPermission(context);            // Get the acccesslevel for this context

            if (access >= Level)                            // If the user's access level is greater than the required level, return success.
                return Task.FromResult(PreconditionResult.FromSuccess());
            else
                return Task.FromResult(PreconditionResult.FromError("Insufficient permissions."));
        }
        
        public AccessLevel GetPermission(ICommandContext c)
        {
            if (c.User.IsBot)                                    // Prevent other bots from executing commands.
                return AccessLevel.Blocked;

            if (Configuration.Load().Owners.Contains(c.User.Id)) // Give configured owners special access.
                return AccessLevel.BotOwner;

            var user = c.User as SocketGuildUser;                // Check if the context is in a guild.
            if (user != null)
            {
                if (c.Guild.OwnerId == user.Id)                  // Check if the user is the guild owner.
                    return AccessLevel.ServerOwner;

                if (user.GuildPermissions.Administrator)         // Check if the user has the administrator permission.
                    return AccessLevel.ServerAdmin;

                if (user.GuildPermissions.ManageMessages ||      // Check if the user can ban, kick, or manage messages.
                    user.GuildPermissions.BanMembers ||
                    user.GuildPermissions.KickMembers)
                    return AccessLevel.ServerMod;
            }

            return AccessLevel.User;                             // If nothing else, return a default permission.
        }
    }
}
