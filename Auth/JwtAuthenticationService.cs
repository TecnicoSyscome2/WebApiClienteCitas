using WebApi.Citas.ClientesApp.Modelos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Citas.ClientesApp.Auth
{
    public class JwtAuthenticationService : IautenticacionService
    {
        private readonly string _key = "123as-dsf23-sfsdf-213123";

        public JwtAuthenticationService(string key)
        {
         _key = key;
       
        }

        public string Authenticate(userModel pUsuario)
        {
            //string _key = Convert.ToString(_configuration.GetSection("_Key"));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim( "UserName", pUsuario.UserName),
                    new Claim( "Password", pUsuario.Password )
                }),
                IssuedAt = DateTime.UtcNow.AddHours(8),
                NotBefore = DateTime.UtcNow.AddMilliseconds(1),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha384Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
