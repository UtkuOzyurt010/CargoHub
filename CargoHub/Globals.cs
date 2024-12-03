using System.Security.Cryptography;
using System.Text;

public static class Globals
{
    public const string Version = "v1";

    public static string get_timestamp()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }

    public static string EncryptApiKey(string key)
    {
        SHA256 mySha565 = SHA256.Create();
        return Encoding.Default.GetString(mySha565.ComputeHash(Encoding.ASCII.GetBytes(key)));
    }

}