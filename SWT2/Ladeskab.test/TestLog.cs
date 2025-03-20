using NUnit.Framework;
using System;
using System.IO;
using Ladeskab.lib.Interfaces;
using Ladeskab.lib;

namespace Ladeskab.test
{
    public class TestFileLogger
    {
        private string testDirectory = "TestLogs";
        private string testLogFileName = "testLog.txt";
        private string testLogFilePath;
        private string nonExistingFilePath = "non_existing_log.txt";
        private string existingFilePath = "existing_log.txt";
        private ILog _uut;

        // Set up the test by creating necessary files and directories
        [SetUp]
        public void SetUp()
        {
            CreateTestDirectory();
            testLogFilePath = Path.Combine(testDirectory, testLogFileName);
            File.Create(testLogFilePath).Close();
            _uut = new FileLogger(testLogFilePath);

            CreateFile(existingFilePath);
            DeleteFileIfExists(nonExistingFilePath);
        }

        // Clean up by deleting files and directories created during tests
        [TearDown]
        public void TearDown()
        {
            DeleteFileIfExists(testLogFilePath);
            DeleteFileIfExists(nonExistingFilePath);
            DeleteFileIfExists(existingFilePath);

            // Remove the test directory if it's empty
            if (Directory.Exists(testDirectory) && Directory.GetFiles(testDirectory).Length == 0)
            {
                Directory.Delete(testDirectory);
            }
        }

        // Test that a log file is created when it already exists
        [Test]
        public void CreateLogFile_FileAlreadyExists()
        {
            var logger1 = new FileLogger(existingFilePath);
            Assert.That(File.Exists(existingFilePath), Is.True);
        }

        // Test that a log file is created when the file does not exist
        [Test]
        public void CreateLogFile_FileNotExists()
        {
            var logger2 = new FileLogger(nonExistingFilePath);
            Assert.That(File.Exists(nonExistingFilePath), Is.True);
        }

        // Test if the "Door Unlocked" message is written to the log file
        [Test]
        public void LogDoorUnlocked_WritesToLogFile()
        {
            int rfidId = 123;
            _uut.LogDoorUnlocked(rfidId);

            AssertLogMessageContains($"Door unlocked by {rfidId}");
        }

        // Test if the "Door Locked" message is written to the log file
        [Test]
        public void LogDoorLocked_WritesToLogFile()
        {
            int rfidId = 456;
            _uut.LogDoorLocked(rfidId);

            AssertLogMessageContains($"Door locked by {rfidId}");
        }

        // Test if both "Door Locked" and "Door Unlocked" messages are written to the log file
        [Test]
        public void LogDoor_WritesToLogFileTwice()
        {
            int rfidId = 123456;

            _uut.LogDoorLocked(rfidId);
            _uut.LogDoorUnlocked(rfidId);

            string[] logLines = File.ReadAllLines(testLogFilePath);
            Assert.That(logLines.Length, Is.EqualTo(2));
            Assert.That(logLines[0].Contains($"Door locked by {rfidId}"), Is.True);
            Assert.That(logLines[1].Contains($"Door unlocked by {rfidId}"), Is.True);
        }

        // Test if reading the log file prints the contents to the console
        [Test]
        public void ReadLog_ReadsLogFileContents()
        {
            string logMessage1 = "Door unlocked by 123";
            string logMessage2 = "Door locked by 456";
            AppendLogMessage(logMessage1);
            AppendLogMessage(logMessage2);

            string consoleOutput = CaptureConsoleOutput(() => _uut.ReadLog());

            Assert.That(consoleOutput.Contains(logMessage1), Is.True);
            Assert.That(consoleOutput.Contains(logMessage2), Is.True);
        }

        // Helper method to create the test directory if it doesn't exist
        private void CreateTestDirectory()
        {
            if (!Directory.Exists(testDirectory))
            {
                Directory.CreateDirectory(testDirectory);
            }
        }

        // Helper method to create a file
        private void CreateFile(string filePath)
        {
            File.Create(filePath).Close();
        }

        // Helper method to delete a file if it exists
        private void DeleteFileIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        // Helper method to assert if the log contains a specific message
        private void AssertLogMessageContains(string message)
        {
            string[] logLines = File.ReadAllLines(testLogFilePath);
            Assert.That(logLines.Length, Is.EqualTo(1));
            Assert.That(logLines[0].Contains(message), Is.True);
        }

        // Helper method to append log messages to the log file
        private void AppendLogMessage(string message)
        {
            File.AppendAllText(testLogFilePath, $"{DateTime.Now}: {message}\n");
        }

        // Helper method to capture the console output during a method call
        private string CaptureConsoleOutput(Action action)
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                action.Invoke();
                return sw.ToString();
            }
        }
    }
}
