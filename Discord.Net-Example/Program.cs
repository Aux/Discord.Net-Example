using Discord;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;
using Discord.Modules;
using Example.Enums;
using Example.Modules;
using Example.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static void Main(string[] args) 
            => new Program().Start();

        private DiscordClient _client;
        
        public void Start()
        {
            EnsureConfigExists();                                       // Create the configuration file and load it into memory.

            _client = new DiscordClient(x =>                            // Create a new instance of DiscordClient
            {
                x.AppName = "ExampleBot";
                x.LogLevel = LogSeverity.Info;                          // Mirror relevant information to the console.
            })
            .UsingCommands(x =>                                         // Configure the Commands extension
            {
                x.PrefixChar = Configuration.Load().Prefix;             // Set the prefix from the configuration file
                x.HelpMode = HelpMode.Public;                           // Enable the automatic `!help` command.
            })
            .UsingPermissionLevels((u, c) => (int)GetPermission(u, c))  // Permission levels are used to check basic or custom permissions
            .UsingModules();                                            // Configure the Modules extension
                                                                        // With LogLevel enabled, mirror info to the console in this format.
            _client.Log.Message += (s, e) => Console.WriteLine($"[{e.Severity}] {e.Source}: {e.Message}");
                                                                        // Load modules into the Modules extension.
            _client.AddModule<ExampleModule>("Example", ModuleFilter.None);
            _client.AddModule<MathModule>("Math", ModuleFilter.None);
            
            _client.ExecuteAndWait(async () =>
            {
                while (true)
                {
                    try
                    {
                        await _client.Connect(Configuration.Load().Token, TokenType.Bot);
                        break;
                    } catch (Exception ex)
                    {
                        _client.Log.Error("Login Failed", ex);
                        await Task.Delay(_client.Config.FailedReconnectDelay);
                    }
                }
            });
        }

        private AccessLevel GetPermission(User user, Channel channel)
        {
            if (user.IsBot)                                     // Prevent other bots from executing commands
                return AccessLevel.Blocked;

            if (Configuration.Load().Owners.Contains(user.Id))  // Create a specific permission for bot owners
                return AccessLevel.BotOwner;

            if (!channel.IsPrivate)                             // Make sure the command isn't executed in a PM.
            {
                if (user == channel.Server.Owner)               // Server owner permission for the server owner.
                    return AccessLevel.ServerOwner;

                if (user.ServerPermissions.Administrator)       // Server admin permission for server admins.
                    return AccessLevel.ServerAdmin;

                if (user.GetPermissions(channel).ManageChannel) // Channel admin permission for channel admins.
                    return AccessLevel.ChannelAdmin;
            }

            return AccessLevel.User;                            // If nothing else fits return a default permission.
        }

        private void EnsureConfigExists()
        {
            if (!Directory.Exists("data"))                      // Check if the data folder exists.
                Directory.CreateDirectory("data");              // Create the data folder.

            string loc = "data/configuration.json";             // Save the file location of the configuration to a string.

            if (!File.Exists(loc))                              // Check if the configuration file exists.
            {
                var config = new Configuration();               // Create a new configuration object.

                Console.WriteLine("The configuration file has been created at 'data\\configuration.json', " +
                              "please enter your information and restart Examplebot.");
                Console.Write("Token: ");

                config.Token = Console.ReadLine();              // Read the bot token from console.
                config.Save();                                  // Save the new configuration object to file.
            }
            Console.WriteLine("Configuration Loaded...");
        }
    }
}
