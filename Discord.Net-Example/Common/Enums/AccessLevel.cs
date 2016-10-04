using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Enums
{
    /// <summary>
    /// The enum used to specify permission levels. A lower
    /// number means less permissions than a higher number.
    /// </summary>
    public enum AccessLevel
    {
        Blocked,
        User,
        ChannelAdmin,
        ServerAdmin,
        ServerOwner,
        BotOwner
    }
}
