using System.ComponentModel.DataAnnotations;

namespace UsersLogin.Models;

public class Games
{
    [Key] // เพิ่ม Primary Key
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = "";

    public string Platform { get; set; } = "";
    public string Developer { get; set; } = "";
    public string Publisher { get; set; } = "";

    public DateTime? OrderDate { get; set; } // เปลี่ยนเป็น nullable
    public DateTime CreatedAt { get; set; }  // เพิ่มฟิลด์นี้
}
