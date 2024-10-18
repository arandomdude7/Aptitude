using System.IO.Compression;
using System.Text.Json;
using System.Net;
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
        UNKNOWN,
        SUCCESS
    }
    public static  InstallErrors InstallPackage(string name)
    {
        WebClient web = new WebClient();
        JsonSerializer.Deserialize<Package>(web.DownloadString(""));
        return InstallErrors.UNKNOWN;
    }
}