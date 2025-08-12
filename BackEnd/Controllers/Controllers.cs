namespace blog;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]

public class AuthContrller : ControllerBase
{
    private readonly BlogContext bContext;

    public AuthContrller(BlogContext inputContext)
    {
        bContext = inputContext;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("AuthCallback")
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("auth-callback")]
    public async Task<IActionResult> AuthCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
        {
            return Unauthorized();
        }


        var email = authenticateResult.Principal.FindFirst(c => c.Type == "email")?.Value;

        if (email == null)
        {
            return BadRequest("Email claim is missing");
        }

        var existingUser = await bContext.UserProfiles.FirstOrDefaultAsync(u => u.Email == email);

        if (existingUser == null)
        {
            return Ok(new
            {
                Message = "User not found. Please create an account.",
                Email = email,
                NeedsAccoutnCreation = true
            });

        }

        return Ok(new
        {
            Message = "Succesful Login.",
            Email = email,
            Username = existingUser.Username,
            UniqueId = existingUser.Id,
            NeedsAccountCreation = false
        });
    }

    [Authorize]
    [HttpPost("create-account")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        var email = User.FindFirst("email")?.Value;

        if (email == null)
        {
            return Unauthorized(new
            {
                Message = "Email could not be found."
            });
        }
        if (await bContext.UserProfiles.AnyAsync(u => u.Username == request.Username))
        {
            return Conflict(new
            {
                Message = "Username already taken."
            });
        }

        if (await bContext.UserProfiles.AnyAsync(u => u.Email == email))
        {
            return BadRequest(new
            {
                Message = "Email already in use."
            });
        }

        var newUser = new UserProfile
        {
            Username = request.Username,
            Bio = request.Bio,
            Email = email
        };

        await bContext.UserProfiles.AddAsync(newUser);

        await bContext.SaveChangesAsync();

        return Ok(new
        {
            Message = "Account succesfully created."
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new
        {
            Message = "Succesfully logged out."
        });
    }

    [Authorize]
    [HttpPost("create-post")]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        var email = User.FindFirst("email")?.Value;
        if (email == null)
        {
            return Unauthorized(new
            {
                Message = "Email could not be found. Please login"
            });
        }

        var user = await bContext.UserProfiles.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            return Unauthorized(new
            {
                Message = "User not authorized."
            });
        }

        var newPost = new Blog
        {
            UserProfileId = user.Id,
            UserProfile = user,
            Content = request.Content,
            Title = request.Title,
            Type = request.Type
        };


        await bContext.AddAsync(newPost);

        await bContext.SaveChangesAsync();

        return Ok(new
        {
            Message = "Blog post created."
        });

    }



}
