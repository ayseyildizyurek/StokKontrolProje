﻿using Microsoft.IdentityModel.Tokens;
using StokKontrolProje.Entities.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StokKontrolProje.WebAPI.Security
{
	public static class TokenHandler
	{
		public static Token CreateToken(User user, IConfiguration configuration)
		{
			List<Claim> claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, Enum.GetName(user.Role))				
			};

			Token token = new Token();
			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
			SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["JWT:Expiration"]));

			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				issuer: configuration["JWT:Issuer"],
				audience: configuration["JWT:Audience"],
				expires: token.Expiration,
				claims: claims,
				notBefore: DateTime.Now,
				signingCredentials: credentials
				);

			JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
			token.AccessToken = securityTokenHandler.WriteToken(jwtSecurityToken);
			return token;
		}
	}
}
