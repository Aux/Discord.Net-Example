using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Example
{
    public class Program
    {
        public static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _commands;

        public async Task Start()
        {
            Configuration.EnsureExists();                    // Ensure the configuration file has been created.
                                                             // Create a new instance of DiscordSocketClient.
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,              // Specify console verbose information level.
                AlwaysDownloadUsers = true,                  // Start the cache off with updated information.
                MessageCacheSize = 1000                      // Tell discord.net how long to store messages (per channel).
            });

            _client.Log += (l)                               // Register the console log event.
                => Task.Run(()
                => Console.WriteLine($"[{l.Severity}] {l.Source}: {l.Exception?.ToString() ?? l.Message}"));
                                                             
            await _client.LoginAsync(TokenType.Bot, Configuration.Load().Token);
            await _client.StartAsync();

            _commands = new CommandHandler();                // Initialize the command handler service
            await _commands.Install(_client);
            
            await Task.Delay(-1);                            // Prevent the console window from closing.
        }
    }
}
