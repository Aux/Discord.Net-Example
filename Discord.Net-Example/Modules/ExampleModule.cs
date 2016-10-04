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
    public class ExampleModule : IModule                                // Inherit the Module interface
    {
        private ModuleManager _manager;                                 // Create variables for manager and client
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;                                         // Initiate variables
            _client = manager.Client;

            manager.CreateCommands("", cmd =>                           // Create commands with the manager
            {
                cmd.CreateCommand("say")                                // The command text `!say <text>`
                    .Description("Make the bot say something")          // The command's description
                    .Alias("s")                                         // An alternate trigger for this command `!s <text>`
                    .MinPermissions((int)AccessLevel.ServerAdmin)       // Limit this command to people who have administrator rights
                    .Parameter("text", ParameterType.Unparsed)          // The parameter for this command
                    .Do(async (e) =>                                    // The code to be run when the command is executed
                    {
                        string text = e.Args[0];                        // Copy the first paramter into a variable
                        await e.Channel.SendMessage(text);              // Send the text to the channel the command was executed in
                    });

                cmd.CreateGroup("set", (b) =>                           // Create a group of sub-commands `!set`
                {
                    b.CreateCommand("nick")                             // The command text `!set nick <name>`
                        .Description("Change your nickname.")       
                        .Parameter("name", ParameterType.Unparsed) 
                        .Do(async (e) =>                           
                        {
                            string name = e.Args[0];                    // Copy the first parameter into a variable
                            var user = e.User;                          // Get the object of the user that executed the command.
                            await user.Edit(nickname: name);            // Edit the user's nickname.
                            await e.Channel.SendMessage(                // Let the user know the command executed successfully.
                                $"{user.Mention} I changed your name to **{name}**");
                        });
                    b.CreateCommand("botnick")                          // The command text `!set botnick <name>`
                        .Description("Change the bot's nickname.")
                        .MinPermissions((int)AccessLevel.ServerOwner)   // Limit this command to server owner
                        .Parameter("name", ParameterType.Unparsed)
                        .Do(async (e) =>
                        {
                            string name = e.Args[0];                    // Copy the first parameter into a variable
                            var bot = e.Server.CurrentUser;             // Get the bot's user object for this server.
                            await bot.Edit(nickname: name);             // Edit the user's nickname.
                            await e.Channel.SendMessage(                // Let the user know the command executed successfully.
                                $"I changed my name to **{name}**");
                        });
                });
            });
        }
    }
}
