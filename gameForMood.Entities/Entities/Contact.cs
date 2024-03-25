namespace gameForMood.Entities;

public partial class Contact
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public int Type { get; set; }

    public string? Logo { get; set; }
}
