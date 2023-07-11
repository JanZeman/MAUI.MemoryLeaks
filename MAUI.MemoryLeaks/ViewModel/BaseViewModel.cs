namespace MAUI.MemoryLeaks.ViewModel;

[ObservableObject]
public abstract partial class BaseViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;

    public virtual void OnAppearing() { }
    public virtual void OnDisappearing() { }
}