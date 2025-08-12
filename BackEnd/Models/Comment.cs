namespace blog;

public class Comment
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}