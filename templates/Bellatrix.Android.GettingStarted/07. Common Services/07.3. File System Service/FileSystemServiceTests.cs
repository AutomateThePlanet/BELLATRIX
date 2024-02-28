using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
public class FileSystemServiceTests : NUnit.AndroidTest
{
    // 1. BELLATRIX gives you an interface for easier work with files using the FileSystemService.
    [Test]
    [Category(Categories.KnownIssue)]
    public void FileSavedToDevice_When_CallPushFile()
    {
        // Creates a new file on the device with the specified text.
        string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
        App.Files.PushFile("/data/local/tmp/remote.txt", data);

        // Returns the content of the specified file as a byte array.
        byte[] returnDataBytes = App.Files.PullFile("/data/local/tmp/remote.txt");
        string returnedData = Encoding.UTF8.GetString(returnDataBytes);

        Assert.That(data.Equals(returnedData));
    }

    [Test]
    [Category(Categories.CI)]
    public void FileSavedToDevice_When_CallPushFileFromBytes()
    {
        string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
        var bytes = Encoding.UTF8.GetBytes(data);

        // Creates a new file on the device from the specified byte array.
        App.Files.PushFile("/data/local/tmp/remote.txt", bytes);

        // Returns the content of the specified file as a byte array.
        byte[] returnDataBytes = App.Files.PullFile("/data/local/tmp/remote.txt");
        string returnedData = Encoding.UTF8.GetString(returnDataBytes);

        Assert.That(data.Equals(returnedData));
    }

    [Test]
    [Category(Categories.KnownIssue)]
    public void FileSavedToDevice_When_CallPushFileFromFileInfo()
    {
        string filePath = Path.GetTempPath();
        var fileName = Guid.NewGuid().ToString();
        string fullPath = Path.Combine(filePath, fileName);

        File.WriteAllText(fullPath,
            "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra");

        try
        {
            var file = new FileInfo(fullPath);

            // Creates a new file on the device from the specified file info.
            App.Files.PushFile("/data/local/tmp/remote.txt", file);

            byte[] returnDataBytes = App.Files.PullFile("/data/local/tmp/remote.txt");
            string returnedData = Encoding.UTF8.GetString(returnDataBytes);
            Assert.That("The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra".Equals(returnedData));
        }
        finally
        {
            File.Delete(fullPath);
        }
    }

    [Test]
    [Category(Categories.KnownIssue)]
    public void AllFilesReturned_When_CallPullFolder()
    {
        string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
        App.Files.PushFile("/data/local/tmp/remote.txt", data);

        // Returns the content of the specified folder as a byte array.
        byte[] returnDataBytes = App.Files.PullFolder("/data/local/tmp/");

        Assert.That(returnDataBytes.Length > 0);
    }
}