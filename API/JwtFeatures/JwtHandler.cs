using Microsoft.IdentityModel.Tokens;
using API.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.JwtFeatures
{
    public class JwtHandler
    {
		private readonly IConfiguration configuration;
		public JwtHandler(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		private SigningCredentials GetSigningCredentials()
		{
			var secretKeyBytes = Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value);

			var secretKey = new SymmetricSecurityKey(secretKeyBytes);

			return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);
		}

		private List<Claim> GetClaims(UserRolesDto userRoleDto)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, userRoleDto.Email),
				new Claim(ClaimTypes.Name, userRoleDto.Name),
				new Claim(ClaimTypes.MobilePhone, userRoleDto.Phone),
				new Claim(ClaimTypes.DateOfBirth, userRoleDto.Birthday.ToString()),
			};

			return claims;
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
		{
			var tokenOptions = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: signingCredentials,
				issuer: configuration.GetSection("Jwt:Issuer").Value,
				audience: configuration.GetSection("Jwt:Audience").Value,
				notBefore: DateTime.Now
			);

			return tokenOptions;
		}

		public string GenerateToken(UserRolesDto userRoleDto)
		{
			var signingCredentials = GetSigningCredentials();
			var claims = GetClaims(userRoleDto);
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return token;
		}
	}
}
