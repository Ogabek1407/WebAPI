using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    [Required]
    public string Email { get; set; }
    public string LastName { get; set; }
    [MinLength(8)]
    public string Password { get; set; }
}