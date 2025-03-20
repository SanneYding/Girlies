using Ladeskab.lib.Interfaces;

namespace Ladeskab.lib;

public class FileLogger : ILog
{
    private readonly string _logFilePath;

    public FileLogger(string filePath)
    {
        _logFilePath = filePath;

        // Ensure log file exists
        if (!File.Exists(_logFilePath))
        {
            File.WriteAllText(_logFilePath, string.Empty);
        }

        // Set file attributes to normal (in case it's read-only)
        File.SetAttributes(_logFilePath, FileAttributes.Normal);
    }

    public void ReadLog()
    {
        // Read and display log contents, if any
        if (new FileInfo(_logFilePath).Length == 0)
        {
            Console.WriteLine("Log file is empty.");
        }
        else
        {
            Console.WriteLine("Log contents:");
            Console.WriteLine(File.ReadAllText(_logFilePath));
        }
    }

    public void LogDoorUnlocked(int rfidId) => LogToFile($"Door unlocked by {rfidId}");

    public void LogDoorLocked(int rfidId) => LogToFile($"Door locked by {rfidId}");

    private void LogToFile(string message)
    {
        // Append log entry with timestamp
        File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
    }
}
