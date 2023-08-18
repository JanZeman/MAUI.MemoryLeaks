namespace MAUI.MemoryLeaks.Model;

public class Item
{
    private static readonly Random Random = new();

    private static int _historyNo;

    public string Value { get; }

    public Item()
    {
        Value = $"{++_historyNo}: {RandomString(7)}";
    }

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public override string ToString()
    {
        return !string.IsNullOrWhiteSpace(Value) ? Value : base.ToString();
    }
}