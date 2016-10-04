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
    [Module, Name("Math")]
    public class MathModule
    {
        private DiscordSocketClient _client;

        public MathModule(DiscordSocketClient client)
        {
            _client = client;
        }

        [Command("isinteger")]
        [Remarks("Check if the input text is a whole number.")]
        [MinPermissions(AccessLevel.User)]
        public async Task IsInteger(IUserMessage msg, int number)
        {
            await msg.Channel.SendMessageAsync($"The text {number} is a number!");
        }

        [Command("multiply")]
        [Remarks("Get the product of two numbers.")]
        [MinPermissions(AccessLevel.User)]
        public async Task Say(IUserMessage msg, int a, int b)
        {
            int product = a * b;
            await msg.Channel.SendMessageAsync($"The product of `{a} * {b}` is `{product}`.");
        }

        [Command("addmany")]
        [Remarks("Get the sum of many numbers")]
        [MinPermissions(AccessLevel.User)]
        public async Task Say(IUserMessage msg, params int[] numbers)
        {
            var sum = numbers.Sum();
            await msg.Channel.SendMessageAsync($"The sum of `{string.Join(", ", numbers)}` is `{sum}`.");
        }
    }
}
