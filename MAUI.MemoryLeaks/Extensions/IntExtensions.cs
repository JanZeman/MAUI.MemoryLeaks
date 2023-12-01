using System.Globalization;

namespace MAUI.MemoryLeaks.Extensions;

public static class IntExtensions
{
    public static string FormatWithDots(this int number)
    {
        var nfi = new NumberFormatInfo { NumberGroupSeparator = ".", NumberDecimalDigits = 0 };
        return number.ToString("N", nfi);
    }
}