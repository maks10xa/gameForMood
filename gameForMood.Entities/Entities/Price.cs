namespace gameForMood.Entities;

public partial class Price
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public int Type { get; set; }

    public string? Price1 { get; set; }
}
