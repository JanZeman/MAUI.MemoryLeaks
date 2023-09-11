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
        // Workaround for the Warning: 'ValueSample' property not found on 'MAUI.MemoryLeaks.ViewModel.Case01ViewModel', target property: 'Microsoft.Maui.Controls.Label.Text'
        if (BindingContext is not ItemSample)
            return;

        base.OnBindingContextChanged();

#if DEBUG
        // Log or debug the change
        Debug.WriteLine($"ItemCard BindingContext is: {BindingContext?.GetType().Name ?? "null"}");
#endif
    }
}