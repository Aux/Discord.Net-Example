using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Modules
{
    [Name("Math")]
    [Summary("Do some math I guess")]
    public class MathModule : ModuleBase<SocketCommandContext>
    {
        [Command("isinteger")]
        [Summary("Check if the input text is a whole number.")]
        public Task IsInteger(int number)
            => ReplyAsync($"The text {number} is a number!");
        
        [Command("multiply")]
        [Summary("Get the product of two numbers.")]
        public async Task Say(int a, int b)
        {
            int product = a * b;
            await ReplyAsync($"The product of `{a} * {b}` is `{product}`.");
        }

        [Command("addmany")]
        [Summary("Get the sum of many numbers")]
        public async Task Say(params int[] numbers)
        {
            int sum = numbers.Sum();
            await ReplyAsync($"The sum of `{string.Join(", ", numbers)}` is `{sum}`.");
        }
    }
}
