using Discord;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Modules;
using Example.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Modules
{
    public class ModeratorModule : IModule                                  // Inherit the Module interface
    {
        private ModuleManager _manager;                                     // Create variables for manager and client
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;                                             // Initiate variables
            _client = manager.Client;

            manager.CreateCommands("", cmd =>                               // Create commands with the manager
            {
                cmd.CreateCommand("kick")                                   // The command text `!kick <user>`
                    .Description("Kick the specified user")                 // The command's description
                    .MinPermissions((int)AccessLevel.ServerAdmin)           // Limit this command to people who have administrator rights
                    .Parameter("user", ParameterType.Unparsed)              // The parameter for this command
                    .Do(async (e) =>                                        // The code to be run when the command is executed
                    {
                        ulong id;                                           // Create a variable to store a user's id.
                        User u = null;                                      // Create a variable to store the user object in
                        string findUser = e.Args[0];                        // Store the parameter in a variable
                        if (!string.IsNullOrWhiteSpace(findUser))           // Make sure user has a value
                        {
                            if (e.Message.MentionedUsers.Count() == 1)      // Check for mentions in the message.
                                u = e.Message.MentionedUsers.FirstOrDefault();
                            else if (e.Server.FindUsers(findUser).Any())    // Check if the user parameter's text matches any users.
                                u = e.Server.FindUsers(findUser).FirstOrDefault();
                            else if (ulong.TryParse(findUser, out id))      // Check if the user parameter's text matches any users.
                                u = e.Server.GetUser(id);
                        }

                        if (u == null)                                      // If no user is found u will be null
                        {
                            await e.Channel.SendMessage($"I was unable to find a user like `{findUser}`");
                            return;
                        }
                                                                            // cya sucker
                        await e.Channel.SendMessage($"cya {u.Mention} :wave:");
                        await u.Kick();
                    });
            });
        }
    }
}