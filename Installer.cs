using System.IO.Compression;
using System.Text.Json;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;
public class Package
{
    public string Name;
    public string URL;
}
public class Installer
{
    public enum InstallErrors {
        NOT_FOUND,
        FAILED_DOWNLOAD,
        SUCCESS
    }
    public static  InstallErrors InstallPackage(string nn)
    {
        WebClient web = new();
        string rr = nn;
        string ee = web.DownloadString("https://raw.githubusercontent.com/arandomdude7/Aptitude/main/index.json");
        Package[] s = JsonConvert.DeserializeObject<Package[]>(ee);
        foreach (var p in s)
        {
            bool r = p.Name.Contains(rr);
            if (r)
            {
                string ss = Guid.NewGuid().ToString();
                string tmp = Path.Combine(Path.GetTempPath(), ss);
                Directory.CreateDirectory(tmp);
                string tmpfile = Path.Combine(tmp, Guid.NewGuid().ToString());
                try
                {
                    web.DownloadFile(p.URL, tmpfile);
                } catch{
                    return InstallErrors.FAILED_DOWNLOAD;
                }
                ZipFile.ExtractToDirectory(tmpfile, tmp);
                File.Delete(tmpfile);
                Process.Start($"cmd.exe", $"/c \"{Path.Combine(tmp, "install.bat")}\"").WaitForExit();
                return InstallErrors.SUCCESS;
            }
        }
        return InstallErrors.NOT_FOUND;
    }
}