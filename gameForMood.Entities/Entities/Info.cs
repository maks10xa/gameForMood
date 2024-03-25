namespace gameForMood.Entities;

public partial class Info
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Value { get; set; }

    public string Location { get; set; } = null!;
}
