using System.Diagnostics;
using MAUI.MemoryLeaks.Extensions;

namespace MAUI.MemoryLeaks.ViewModel;

public abstract partial class BaseViewModel : ObservableObject
{
    private const int RefreshInfoInSeconds = 1;
    private const int CallGarbageCollectorInSeconds = 6;
    private const int OriginalMemoryCounterMax = 1;

    private readonly Timer _refreshInfoTimer, _callGarbageCollectorTimer;

    private int _originalMemoryCounter;
    private int _garbageCollectorCountdown = CallGarbageCollectorInSeconds;

    [ObservableProperty]
    private string _pageName;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    private string _originalMemoryUsageDescription = "Original memory usage: ";

    [ObservableProperty]
    private string _originalMemoryUsage;

    [ObservableProperty]
    private string _currentMemoryUsageDescription = "Current memory usage: ";

    [ObservableProperty]
    private string _currentMemoryUsage;

    [ObservableProperty]
    private string _garbageCollectorIllustration;

    [ObservableProperty]
    private string _recommendation;

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
        CurrentMemoryUsage = GetCurrentMemoryUsage();
        GarbageCollectorIllustration = UpdateGarbageCollectorIllustration();
        if (_originalMemoryCounter++ > OriginalMemoryCounterMax) return;
        OriginalMemoryUsage = GetOriginalMemoryUsage();
    }

    private string UpdateGarbageCollectorIllustration()
    {
        var value = "Now";

        if (_garbageCollectorCountdown-- > 1)
            value = $"{_garbageCollectorCountdown} s";
        else
            _garbageCollectorCountdown = CallGarbageCollectorInSeconds;

        
        return $"Explicit garbage collection: {value}";
    }

    protected static string GetOriginalMemoryUsage()
    {
        return $"{GetMemoryUsage().FormatBytes()}";
    }

    private static string GetCurrentMemoryUsage()
    {
        return $"{GetMemoryUsage().FormatBytes()}";
    }

    private static long GetMemoryUsage() => Process.GetCurrentProcess().PrivateMemorySize64;
}