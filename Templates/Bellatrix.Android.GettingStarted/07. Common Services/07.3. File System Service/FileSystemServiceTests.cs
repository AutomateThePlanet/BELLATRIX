using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".ApiDemos",
        AppBehavior.ReuseIfStarted)]
    public class FileSystemServiceTests : AndroidTest
    {
        // 1. BELLATRIX gives you an interface for easier work with files using the FileSystemService.
        [TestMethod]
        [TestCategory(Categories.KnownIssue)]
        public void FileSavedToDevice_When_CallPushFile()
        {
            // Creates a new file on the device with the specified text.
            string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
            App.FileSystemService.PushFile("/data/local/tmp/remote.txt", data);

            // Returns the content of the specified file as a byte array.
            byte[] returnDataBytes = App.FileSystemService.PullFile("/data/local/tmp/remote.txt");
            string returnedData = Encoding.UTF8.GetString(returnDataBytes);

            Assert.AreEqual(data, returnedData);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void FileSavedToDevice_When_CallPushFileFromBytes()
        {
            string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
            var bytes = Encoding.UTF8.GetBytes(data);

            // Creates a new file on the device from the specified byte array.
            App.FileSystemService.PushFile("/data/local/tmp/remote.txt", bytes);

            // Returns the content of the specified file as a byte array.
            byte[] returnDataBytes = App.FileSystemService.PullFile("/data/local/tmp/remote.txt");
            string returnedData = Encoding.UTF8.GetString(returnDataBytes);

            Assert.AreEqual(data, returnedData);
        }

        [TestMethod]
        [TestCategory(Categories.KnownIssue)]
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
                App.FileSystemService.PushFile("/data/local/tmp/remote.txt", file);

                byte[] returnDataBytes = App.FileSystemService.PullFile("/data/local/tmp/remote.txt");
                string returnedData = Encoding.UTF8.GetString(returnDataBytes);
                Assert.AreEqual(
                    "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra",
                    returnedData);
            }
            finally
            {
                File.Delete(fullPath);
            }
        }

        [TestMethod]
        [TestCategory(Categories.KnownIssue)]
        public void AllFilesReturned_When_CallPullFolder()
        {
            string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
            App.FileSystemService.PushFile("/data/local/tmp/remote.txt", data);

            // Returns the content of the specified folder as a byte array.
            byte[] returnDataBytes = App.FileSystemService.PullFolder("/data/local/tmp/");

            Assert.IsTrue(returnDataBytes.Length > 0);
        }
    }
}