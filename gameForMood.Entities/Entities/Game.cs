namespace gameForMood.Entities;

public partial class Game
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int GenreId { get; set; }

    public string? Image { get; set; }

    public string Info { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}
