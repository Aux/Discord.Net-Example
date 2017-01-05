using Discord.Commands;
using Example.Attributes;
using Example.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Modules
{
    [Name("Math")]
    public class MathModule : ModuleBase
    {
        [Command("isinteger")]
        [Remarks("Check if the input text is a whole number.")]
        [MinPermissions(AccessLevel.User)]
        public async Task IsInteger(int number)
        {
            await ReplyAsync($"The text {number} is a number!");
        }

        [Command("multiply")]
        [Remarks("Get the product of two numbers.")]
        [MinPermissions(AccessLevel.User)]
        public async Task Say(int a, int b)
        {
            int product = a * b;
            await ReplyAsync($"The product of `{a} * {b}` is `{product}`.");
        }

        [Command("addmany")]
        [Remarks("Get the sum of many numbers")]
        [MinPermissions(AccessLevel.User)]
        public async Task Say(params int[] numbers)
        {
            int sum = numbers.Sum();
            await ReplyAsync($"The sum of `{string.Join(", ", numbers)}` is `{sum}`.");
        }
    }
}
