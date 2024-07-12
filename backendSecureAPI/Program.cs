using Controllers;

var builder = WebApplication.CreateBuilder(args);
string _key = "";

builder.Services.AddAuthorization();
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
        var dbController = new DataBaseController();
        bool userRegistered = dbController.RegisterUser(username, userpassword);
        return userRegistered ? Results.Ok("User registered successfully") : Results.Conflict("User registered unsuccessfully");
    }

    return Results.BadRequest("No se hizo nada");
});

app.Run();