using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;

using (var regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
{
    if (Assembly.GetEntryAssembly() is Assembly assembly)
    {
        var program = assembly.GetName().Name;

        if (regKey != null && string.IsNullOrEmpty(program) is false)
            try
            {
                var fileName = string.Concat(assembly.ManifestModule.Name[..^4], ".exe");

                if (regKey.GetValue(program) is null && string.IsNullOrEmpty(fileName) is false)
                {
                    regKey.SetValue(program, Path.Combine(Environment.CurrentDirectory, fileName));
                }
                /*
                else
                {
                    regKey.DeleteValue(program, false);
                }
                */
                regKey.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
    }
}
using (var process = new Process
{
    StartInfo = new ProcessStartInfo
    {
        WorkingDirectory = "C:\\dev\\nginx-1.22.0",
        Verb = "runas",
        FileName = "nginx.exe",
        UseShellExecute = true
    }
})
{
    if (process.Start())
    {
        Console.WriteLine("nginx");
    }
    else
    {
        Console.WriteLine("failed");
    }
}
using (var process = new Process
{
    StartInfo = new ProcessStartInfo
    {
        WorkingDirectory = "C:\\",
        Verb = "runas",
        FileName = "pwsh",
        UseShellExecute = false,
        RedirectStandardInput = true,
        RedirectStandardOutput = true,
        CreateNoWindow = true
    }
})
{
    process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);
    process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);

    if (process.Start())
    {
        process.BeginOutputReadLine();
        process.StandardInput.Write(@"ngrok tunnel --label edge=edghts_2PzZulUqyinuNeuIZXlF16SS1rk http://localhost:80" + Environment.NewLine);
        process.StandardInput.Close();
        process.WaitForExit();
    }
    else
    {
        Console.WriteLine("failed");
    }
    Console.WriteLine("ngrok");
}