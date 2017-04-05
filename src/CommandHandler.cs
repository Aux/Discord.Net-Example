using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;

namespace Example
{
    /// <summary> Detect whether a message is a command, then execute it. </summary>
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _cmds;

        public async Task InstallAsync(DiscordSocketClient c)
        {
            _client = c;                                                 // Save an instance of the discord client.
            _cmds = new CommandService();                                // Create a new instance of the commandservice.                              
            
            await _cmds.AddModulesAsync(Assembly.GetEntryAssembly());    // Load all modules from the assembly.
            
            _client.MessageReceived += HandleCommandAsync;               // Register the messagereceived event to handle commands.
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)                                          // Check if the received message is from a user.
                return;
            
            var context = new SocketCommandContext(_client, msg);     // Create a new command context.

            int argPos = 0;                                           // Check if the message has either a string or mention prefix.
            if (msg.HasStringPrefix(Configuration.Load().Prefix, ref argPos) ||
                msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {                                                         // Try and execute a command with the given context.
                var result = await _cmds.ExecuteAsync(context, argPos);

                if (!result.IsSuccess)                                // If execution failed, reply with the error message.
                    await context.Channel.SendMessageAsync(result.ToString());
            }
        }
    }
}
