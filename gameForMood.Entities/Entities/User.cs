namespace gameForMood.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public string? Image { get; set; }

    public string? Email { get; set; }
}
