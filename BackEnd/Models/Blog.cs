namespace blog;

public class Blog
{
    public int Id { get; set; } //primary key
    public int UserProfileId { get; set; } //foreign key to user
    public UserProfile UserProfile { get; set; }  //navigation property
    public string? Content { get; set; }
    public string Title { get; set; }
    public int Likes { get; set; } = 0;
    public string Type { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();



}