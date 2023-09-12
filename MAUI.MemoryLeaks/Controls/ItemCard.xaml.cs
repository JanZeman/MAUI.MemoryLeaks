using System.Diagnostics;
using MAUI.MemoryLeaks.Model;

namespace MAUI.MemoryLeaks.Controls;

public partial class ItemCard
{
	public ItemCard()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        // Workaround for the 'not found' warning when BindingContext is temporarily set by the MAUI framework to ViewModel (visual inheritance) instead to the expected class
        if (BindingContext is not ItemSample)
            return;

        base.OnBindingContextChanged();

#if DEBUG
        // Log or debug the change
        Debug.WriteLine($"ItemCard BindingContext is: {BindingContext?.GetType().Name ?? "null"}");
#endif
    }
}