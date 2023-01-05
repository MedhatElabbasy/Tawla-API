using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tawala.Domain.Entities.Identity;
using Tawala.Infrastructure.Services;

namespace Tawala.Application.Services.AuthService
{
    public class JwtHandler
    {
        private readonly MyConfigService myConfigService;
        private readonly UserManager<AppUser> userManager;

        public JwtHandler(MyConfigService myConfigService, UserManager<AppUser> userManager)
        {
            this.myConfigService = myConfigService;
            this.userManager = userManager;
        }
        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(myConfigService.options.JWTSettings.securityKey);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public async Task<List<Claim>> GetClaimsAsync(AppUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id) };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: myConfigService.options.JWTSettings.validIssuer,
                audience: myConfigService.options.JWTSettings.validAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(myConfigService.options.JWTSettings.expiryInMinutes)),
                signingCredentials: signingCredentials);
            return tokenOptions;
        }
    }
}
