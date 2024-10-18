if (args[0] == "install")
{
    using (ProgressBar bar = new ProgressBar())
    {
        for (int i = 1;i<args.Length;i++)
        {
            string c = args[i];
            bar.UpdatePrefix("Installing " + c + " " + ((i-1).ToString()) + " / " + ((args.Length-1).ToString()));
            Installer.InstallPackage(c);
            bar.Report(i / (args.Length));
        }
    }
}