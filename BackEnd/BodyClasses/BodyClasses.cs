namespace blog;

public class CreateAccountRequest()
{
    public string Username { get; set; }
    public string? Bio { get; set; }

}

public class CreatePostRequest()
{
    public string? Content { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }

}