public static class Globals
{
    public const string Version = "v1";

    public static string get_timestamp()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }
}