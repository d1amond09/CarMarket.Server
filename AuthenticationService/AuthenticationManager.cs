using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationService;

public class AuthenticationManager(UserManager<User> userManager, IConfiguration configuration) : IAuthenticationManager
{
	private readonly UserManager<User> _userManager = userManager;
	private readonly IConfiguration _configuration = configuration;
	private User? _user;

	public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
	{
		_user = await _userManager.FindByNameAsync(userForAuth.UserName);
		return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
	}

	public async Task<string> CreateToken()
	{
		var signingCredentials = GetSigningCredentials();
		var claims = await GetClaims();
		var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}
	private SigningCredentials GetSigningCredentials()
	{
		byte[] key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRETKEYCARMARKET"));
		var secret = new SymmetricSecurityKey(key);
		return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
	}
	private async Task<List<Claim>> GetClaims()
	{
		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, _user.UserName)
		};
		var roles = await _userManager.GetRolesAsync(_user);
		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}
		return claims;

	}
	private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
	{
		var jwtSettings = _configuration.GetSection("JwtSettings");
		var tokenOptions = new JwtSecurityToken
		(
			issuer: jwtSettings.GetSection("validIssuer").Value,
			audience: jwtSettings.GetSection("validAudience").Value,
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)), 
			signingCredentials: signingCredentials
		);
		return tokenOptions;
	}
}

