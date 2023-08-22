namespace MAUI.MemoryLeaks.Model;

public class Item
{
    ////private static readonly List<Item> SimulatedLeakedObjects = new ();

    private static readonly Random Random = new();

    private static int _historyNo;

    public string Value { get; }

    public Item()
    {
        Value = $"{++_historyNo}: {RandomString(7)}";

        // Every time an Item instance is created, it adds itself to a static list.
        // This means even if you lose all other references to a that instance, it's still
        // referenced by this static list and won't be garbage collected.
        ////SimulatedLeakedObjects.Add(this);
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