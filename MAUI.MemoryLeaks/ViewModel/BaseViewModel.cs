using System.Diagnostics;
using System.Reflection;

namespace MAUI.MemoryLeaks.ViewModel;

[ObservableObject]
public abstract partial class BaseViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    private string _memorySize;

    public virtual void OnAppearing() => UpdateInfo();
    public virtual void OnDisappearing() { }

    protected virtual void UpdateInfo()
    {
        MemorySize = UpdateMemoryUsage(null);
    }

    private static string UpdateMemoryUsage(object state)
    {
        // Get the current process
        var currentProcess = Process.GetCurrentProcess();

        // Get the working set (physical memory usage) of the process
        var memoryUsage = currentProcess.PrivateMemorySize64;

        // Convert bytes to a more human-readable format
        var formattedMemoryUsage = FormatBytes(memoryUsage);

        // Display the memory usage
        return $"Private memory size: {formattedMemoryUsage}";
    }

    private static string FormatBytes(long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
        var suffixIndex = 0;

        double adjustedBytes = bytes;

        while (adjustedBytes >= 1024 && suffixIndex < suffixes.Length - 1)
        {
            adjustedBytes /= 1024;
            suffixIndex++;
        }

        return $"{adjustedBytes:0.##} {suffixes[suffixIndex]}";
    }

    private string GetMemoryUsage()
    {
        try
        {
            var memory = GC.GetTotalMemory(true);

            string fname = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);

            ProcessStartInfo ps = new ProcessStartInfo("tasklist");
            ps.Arguments = "/fi \"IMAGENAME eq " + fname + ".*\" /FO CSV /NH";
            ps.RedirectStandardOutput = true;
            ps.CreateNoWindow = true;
            ps.UseShellExecute = false;
            var p = Process.Start(ps);
            if (p.WaitForExit(1000))
            {
                var s = p.StandardOutput.ReadToEnd().Split('\"');
                var result = s[9].Replace("\"", "");
                return result;
            }
        }
        catch { }
        return "Unable to get memory usage";
    }
}