using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Types
{
    /// <summary> 
    /// A file that contains information you either don't want public
    /// or will want to change without having to compile another bot.
    /// </summary>
    public class Configuration
    {
        /// <summary> Your bot's command prefix. Please don't pick `!`. </summary>
        public char Prefix { get; set; }
        /// <summary> Ids of users who will have owner access to the bot. </summary>
        public ulong[] Owners { get; set; }
        /// <summary> Your bot's login token. </summary>
        public string Token { get; set; }

        public Configuration()
        {
            Prefix = '!';
            Owners = new ulong[] { 0 };
            Token = "";
        }

        /// <summary> Save the current configuration object to a file. </summary>
        /// <param name="loc"> The configuration file's location. </param>
        public void SaveFile(string loc)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            if (!File.Exists(loc))
                File.Create(loc).Close();

            File.WriteAllText(loc, json);
        }

        /// <summary> Load the information saved in your configuration file. </summary>
        /// <param name="loc"> The configuration file's location. </param>
        public static Configuration LoadFile(string loc)
        {
            string json = File.ReadAllText(loc);
            return JsonConvert.DeserializeObject<Configuration>(json);
        }
    }
}
