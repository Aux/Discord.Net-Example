namespace Example
{
    /// <summary>
    /// The enum used to specify permission levels. A lower
    /// number means less permissions than a higher number.
    /// </summary>
    public enum AccessLevel
    {
        Blocked,
        User,
        ServerMod,
        ServerAdmin,
        ServerOwner,
        BotOwner
    }
}
