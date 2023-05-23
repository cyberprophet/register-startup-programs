using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;

using (var process = new Process
{
    StartInfo = new ProcessStartInfo
    {
        WorkingDirectory = "C:\\",
        Verb = "runas",
        FileName = "cmd",
        UseShellExecute = false,
        RedirectStandardInput = true,
        RedirectStandardOutput = true,
        CreateNoWindow = true
    }
})
{
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
}
using (var regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
{
    if (Assembly.GetEntryAssembly() is Assembly assembly)
    {
        var program = assembly.GetName().Name;

        if (regKey != null && string.IsNullOrEmpty(program) is false)
            try
            {
                var fileName = string.Concat(assembly.ManifestModule.Name[..^4], ".exe");

                Console.WriteLine("'Y'를 입력하면 시작프로그램에 등록합니다.");

                if (ConsoleKey.Y == Console.ReadKey().Key)
                {
                    if (regKey.GetValue(program) is null && string.IsNullOrEmpty(fileName) is false)
                    {
                        regKey.SetValue(program, Path.Combine(Environment.CurrentDirectory, fileName));
                    }
                }
                else
                {
                    regKey.DeleteValue(program, false);
                }
                regKey.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
    }
}