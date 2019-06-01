using System.IO;

public static class Debug
{
    private static StreamWriter writer;

    public static void Init()
    {
        writer = new StreamWriter("debug.txt");
    }

    public static void Log(string message)
    {
        writer.WriteLine(message);
        writer.Flush();
    }
}