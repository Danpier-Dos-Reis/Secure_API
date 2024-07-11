using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocalServices
{
    public class TokenMaker
    {
        /// <summary>
        /// Make a user token
        /// </summary>
        /// <returns>SHA256 Token like string</returns>
        public string MakeToken(string _key, string user, string password){

            var tokenHandler = new JwtSecurityTokenHandler();
            var byteKey = Encoding.UTF8.GetBytes(_key);
            var tokenDes = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user), new Claim("Password",password)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDes);
            return tokenHandler.WriteToken(token);
        }
    }
}