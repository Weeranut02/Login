namespace UsersLogin.Models;


public class GamesDto
{
    public int Id { get; set; }
    public string? Title { get; set; } = "";
    public string? Platform { get; set; } = "";
    public string? Developer { get; set; } = "";
    public string? Publisher { get; set; } = "";
    public DateTime? OrderDate { get; set; }
    public DateTime CreatedAt { get; set; }


}
