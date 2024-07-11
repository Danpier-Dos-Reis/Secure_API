using Controllers;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
string _key = "";

builder.Services.AddAuthorization();
var app = builder.Build();

app.MapGet("/auth/{username}/{userpassword}/{option}", (string username, string userpassword, int option) => {
    
    bool userRegistered = false;

    //Crea el token si es que el usuario ya está registrado
    if(option == 0){
        _key = new TokenController().CreateToken(username,userpassword);
        return (!string.IsNullOrEmpty(_key))? _key:"User don't exist";;
    }

    //Registra al usuario en la DB
    if(option == 1){
        var dbController = new DataBaseController();
        userRegistered = dbController.RegisterUser(username, userpassword);
        return (userRegistered == true) ? "User registered successfully":"User registered unsuccessfully";
    }
    else{return "No se hizo nada";}
});

app.Run();