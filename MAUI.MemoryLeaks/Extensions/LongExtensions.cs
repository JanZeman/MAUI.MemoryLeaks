using System.Globalization;

namespace MAUI.MemoryLeaks.Extensions;

public static class LongExtensions
{
    public static string FormatBytes(this long bytes)
    {
        string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
        var suffixIndex = 0;

        double adjustedBytes = bytes;

        while (adjustedBytes >= 1024 && suffixIndex < suffixes.Length - 1)
        {
            adjustedBytes /= 1024;
            suffixIndex++;
        }

        return $"{adjustedBytes:0.0} {suffixes[suffixIndex]}";
    }
}