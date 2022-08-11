using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.Core.Utilities;

public static class FileDownloader
{
    public static string Download(string url, string fullFilePath, bool shouldCache = true)
    {
        if (shouldCache && File.Exists(fullFilePath))
        {
            return fullFilePath;
        }

        try
        {
            using var httpClient = new HttpClient();
            var fileInfo = new FileInfo(fullFilePath);
            var response = httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            using var ms = response.Content.ReadAsStreamAsync().Result;
            using var fs = File.Create(fileInfo.FullName);
            ms.Seek(0, SeekOrigin.Begin);
            ms.CopyTo(fs);

            return fileInfo.FullName;
        }
        catch (Exception e)
        {
            e.PrintStackTrace();
            return string.Empty;
        }
    }

    public static string DownloadToAppData(string url, bool shouldCache = true)
    {
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string fileName = url.Split('/').Last();
        string fullFileName = Path.Combine(appdata, fileName);
        return Download(url, fullFileName, shouldCache);
    }
}
