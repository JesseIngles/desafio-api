using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using api.DAL.Entities;

namespace api.Helpers
{
  public static class JwtHelper
  {
    private static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Tchilla".PadRight(128)));
    public static string GerarTokenUsuario(TbUsuario usuario)
    {
      var creds = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

      var claims = new List<Claim>
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("userId", usuario.Id.ToString()),
                new Claim("papel", "Usuario"),
                new Claim("nome", usuario.Nome),
            };

      var token = new JwtSecurityToken(
          issuer: "Tchilla",
          audience: "Tchilla",
          claims: claims,
          expires: DateTime.Now.AddMonths(6),
          signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
