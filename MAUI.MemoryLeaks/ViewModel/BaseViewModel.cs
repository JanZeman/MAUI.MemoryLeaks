using System.Diagnostics;

namespace MAUI.MemoryLeaks.ViewModel;

public abstract partial class BaseViewModel : ObservableObject
{
    private const int RefreshInfoInSeconds = 1;
    private const int CallGarbageCollectorInSeconds = 5;
    protected const int TestItemsCollectionCount = 1000 * 1000;

    private readonly Timer _refreshInfoTimer, _callGarbageCollectorTimer;

    [ObservableProperty]
    private string _pageName;

    [ObservableProperty]
    private string _description;

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

    /// <summary>
    /// Attempt to execute 'aggressive' garbage collection
    /// </summary>
    private static void CallGarbageCollector()
    {
        // Collect all generations with forced mode
        GC.Collect(2, GCCollectionMode.Forced);

        // Wait for all finalizers to complete before continuing
        // This ensures that all finalizable objects are finalized.
        GC.WaitForPendingFinalizers();

        // Compact the Large Object Heap (LOH)
        // This is available starting from .NET 5. If you're using an earlier version, 
        // you might want to conditionally compile this or remove it.
        GC.Collect(2, GCCollectionMode.Forced, true, true);
    }

    protected virtual void RefreshInfo()
    {
        MemorySize = UpdateMemoryUsage();
    }

    private static string UpdateMemoryUsage()
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

        return $"{adjustedBytes:0.00} {suffixes[suffixIndex]}";
    }
}