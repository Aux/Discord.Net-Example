using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Types
{
    /// <summary> 
    /// A file that contains information you either don't want public
    /// or will want to change without having to compile another bot.
    /// </summary>
    public class Configuration
    {
        /// <summary> The location of your bot's dll, ignored by the json parser. </summary>
        [JsonIgnore]
        public static readonly string appdir = AppContext.BaseDirectory;

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
        /// <param name="file"> The configuration file's location. </param>
        public void SaveFile(string file)
        {
            string loc = Path.Combine(appdir, file);
            string json = ToJson();
            File.WriteAllText(loc, json);
        }

        /// <summary> Load the information saved in your configuration file. </summary>
        /// <param name="file"> The configuration file's location. </param>
        public static Configuration LoadFile(string file)
        {
            string loc = Path.Combine(appdir, file);
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(loc));
        }

        /// <summary> A quick method to parse the configuration to a json string. </summary>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
