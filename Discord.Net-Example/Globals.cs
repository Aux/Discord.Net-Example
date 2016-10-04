using Discord;
using Discord.Commands;
using Example.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Example
{
    public class Globals
    {
        public static Configuration Config { get; set; }
        public static CommandService CommandService { get; set; }

        public static void EnsureConfigExists()
        {
            string loc = Path.Combine(AppContext.BaseDirectory, "configuration.json");

            if (!File.Exists(loc))
            {
                Config = new Configuration();
                Config.SaveFile(loc);

                Console.WriteLine("The example bot's configuration file has been created. Please enter a valid token.");
                Console.Write("Token: ");

                Config.Token = Console.ReadLine();                     // Read the user's token from the console.
                Config.SaveFile(loc);
            }
        }
        
        public static void LoadConfig()
        {
            Config = Configuration.LoadFile(@"data\configuration.json");
        }
    }
}
