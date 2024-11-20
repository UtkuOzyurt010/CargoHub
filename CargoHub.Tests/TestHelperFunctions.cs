public static class TestHelperFunctions
{
    public static void WriteLogToFile(string filePath, string message)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(message);
        }
    }
}