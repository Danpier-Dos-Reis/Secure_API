using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Controllers;

var builder = WebApplication.CreateBuilder(args);

var _dbController = new DataBaseController();
List<string> _keys = _dbController.GetAllUserKeys();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
    var signingKeys = _keys.Select(key => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))).ToList();
    var signingCredentials = signingKeys.Select(signingKey => new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)).ToList();

    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
        {
            // Return all signing keys
            return signingKeys;
        }
    };
});



var app = builder.Build();

app.MapGet("/auth/{username}/{userpassword}/{option}", (string username, string userpassword, int option) =>
{
    if (option == 0)
    {
        string token = new TokenController().CreateToken(username, userpassword);
        return !string.IsNullOrEmpty(token) ? Results.Ok(token) : Results.Unauthorized();
    }

    if (option == 1)
    {
        bool userRegistered = _dbController.RegisterUser(username);
        return userRegistered ? Results.Ok("User registered successfully") : Results.Conflict("User registered unsuccessfully");
    }

    return Results.BadRequest("No se hizo nada");
});

app.MapGet("/private/content",()=>{return Results.Ok("<h1>Aquellos que se dejan cuidar dotan de sentido a sus cuidadores</h1>");})
    .RequireAuthorization();

app.Run();