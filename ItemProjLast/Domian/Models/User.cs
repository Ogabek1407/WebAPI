using ItemProjLast.Domian.Enums;

namespace Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Gmail { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Role Role { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}