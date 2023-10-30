using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Tarefas.API.Interfaces.Services
{
	public class DecriptaService : IDecripta
	{
		private readonly IConfiguration _configuration;
		public DecriptaService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string DecriptaUsuario(string header)
		{
			string token = header.Split().Last();
			string secret = _configuration["Jwt:key"];
			var key = Encoding.UTF8.GetBytes(secret);
			var handler = new JwtSecurityTokenHandler();
			var validations = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false
			};
			var claims = handler.ValidateToken(token, validations, out var tokenSecure);
			return claims.Identity.Name;
		}
	}
}
