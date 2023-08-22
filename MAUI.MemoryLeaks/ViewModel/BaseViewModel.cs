using System.Diagnostics;
using System.Reflection;

namespace MAUI.MemoryLeaks.ViewModel;

public abstract partial class BaseViewModel : ObservableObject
{
    private const int RefreshInfoInSeconds = 1;
    private const int CallGarbageCollectorInSeconds = 3;

    private readonly Timer _refreshInfoTimer, _callGarbageCollectorTimer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    private string _memorySize;

    protected BaseViewModel()
    {
        _refreshInfoTimer = new Timer(_ => MainThread.InvokeOnMainThreadAsync(RefreshInfo), null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        _callGarbageCollectorTimer = new Timer(_ => MainThread.InvokeOnMainThreadAsync(CallGarbageCollector), null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
    }

    public virtual void OnAppearing()
    {
        // Start the timers
        _refreshInfoTimer.Change(TimeSpan.FromSeconds(RefreshInfoInSeconds), TimeSpan.FromSeconds(RefreshInfoInSeconds));
        _callGarbageCollectorTimer.Change(TimeSpan.FromSeconds(CallGarbageCollectorInSeconds), TimeSpan.FromSeconds(CallGarbageCollectorInSeconds));
    }

    public virtual void OnLoaded()
    {
        RefreshInfo();
    }

    public virtual void OnDisappearing()
    {
        // Stop the timers
        _refreshInfoTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        _callGarbageCollectorTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
    }

    private static void CallGarbageCollector()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    protected virtual void RefreshInfo()
    {
        //CallGarbageCollector();
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