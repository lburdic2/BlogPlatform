namespace blog;
public class UserProfile
{
    public int Id { get; set; } //primary key
    public string Username { get; set; }
    public string? Bio { get; set; }
    public string Email { get; set; }
    public ICollection<Blog> Blogs { get; set; } = new List<Blog>();

}