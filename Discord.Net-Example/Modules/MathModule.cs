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
    public class MathModule : IModule                                      // Inherit the Module interface
    {
        private ModuleManager _manager;                                         // Create variables for manager and client
        private DiscordClient _client;

        void IModule.Install(ModuleManager manager)
        {
            _manager = manager;                                                 // Initiate variables
            _client = manager.Client;

            manager.CreateCommands("", cmd =>                                   // Create commands with the manager
            {
                cmd.CreateCommand("isinteger")                                  // The command text `!isinteger <int>`
                    .Description("Check if the input text is a whole number.")  // The command's description
                    .MinPermissions((int)AccessLevel.User)                      // 
                    .Parameter("int", ParameterType.Required)                   // The parameter for this command
                    .Do(async (e) =>                                            // The code to be run when the command is executed
                    {
                        string text = e.Args[0];                                // Copy the first parameter into a variable
                        
                        int number;                                             // The variable to parse the integer into
                        if (int.TryParse(text, out number))                     // Boolean check if the string is an integer
                            await e.Channel.SendMessage($"The text `{text}` is a number!");
                        else
                            await e.Channel.SendMessage($"The text `{text}` is not a number!");
                    });
                cmd.CreateCommand("multiply")                                   // The command text `!multiply <a> <b>`
                    .Description("Get the product of two numbers.")             // The command's description
                    .Parameter("a", ParameterType.Required)                     // The parameter for this command
                    .Parameter("b", ParameterType.Required)                     // The parameter for this command
                    .Do(async (e) =>                                            // The code to be run when the command is executed
                    {
                        string a = e.Args[0];                                   // Copy the first parameter into a variable
                        string b = e.Args[1];                                   // Copy the second parameter into a variable

                        int first, second;                                      // The variable to parse the integer into
                        if (int.TryParse(a, out first) && int.TryParse(b, out second))
                        {
                            int product = first * second;                       // Save the product into it's own variable
                            await e.Channel.SendMessage($"The product of `{a} * {b}` is `{product}`.");
                        } else
                        {
                            await e.Channel.SendMessage($"I cannot multiply `{a}` with `{b}`");
                        }
                    });
                cmd.CreateCommand("addmany")                                    // The command text `!addmany <numbers...>`
                    .Description("Get the sum of many numbers")                 // The command's description
                    .Parameter("numbers", ParameterType.Multiple)               // The parameter for this command
                    .Do(async (e) =>                                            // The code to be run when the command is executed
                    {
                        string[] numbers = e.Args;                              // Copy the parameters into a variable

                        int sum = 0;                                            // The variable to store the sum in
                        foreach(string number in numbers)                       // Loop through all the parameters in the array.
                        {
                            int parsed;
                            if (int.TryParse(number, out parsed))
                            {
                                sum += parsed;                                  // Add to sum if it's a number.
                            }
                            else
                            {                                                   // Break if a non-number is found.
                                await e.Channel.SendMessage($"The value `{number}` is not a number.");
                                return;
                            }
                        }

                        await e.Channel.SendMessage($"The sum of `{string.Join(", ", numbers)}` is `{sum}`.");
                    });
            });
        }
    }
}