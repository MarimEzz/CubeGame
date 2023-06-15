﻿using CubeGame.DAL.Data.Models.Account;
using CubeGame.Data.Helper;
using CubeGame.Data.Models;
using CubeGame.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CubeGame.DAL.Repo.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            //_roleManager = roleManager;
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            //var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;


           // authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            authModel.Token = user.token;
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            //authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = user.Role;

            //Is the user that login to App has any active refresh token ?

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = activeRefreshToken.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                user.token = authModel.Token;    /*  ----------------------------------------------*/
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;


                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            return authModel;
        }

        //public async Task<string> AddRoleAsync(AddRoleModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.UserId);

        //    if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
        //        return "Invalid user ID or Role";

        //    if (await _userManager.IsInRoleAsync(user, model.Role))
        //        return "User already assigned to this role";

        //    var result = await _userManager.AddToRoleAsync(user, model.Role);

        //    return result.Succeeded ? string.Empty : "Something went wrong";
        //}


        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            else if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }

            //await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);
           
            var refreshToken = GenerateRefreshToken();

            //////////////////////////////////////////////////////////////////////////////////
            user.token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            ///////////////////////////////////////////////////////////////////////////////
            
            user.RefreshTokens?.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            return new AuthModel
            {
                Email = user.Email,
               // ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                //Roles = new List<string> { "User" },
                Roles = user.Role,
                Token = user.token,
                Username = user.UserName,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn
            };

            //throw new NotImplementedException();
        }


        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();

            // get the user that has the token that has sent
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                authModel.Message = "Invalid token";
                return authModel;
            }

            // after we find user , chech if his token is active or not
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authModel.Message = "Inactive token";
                return authModel;
            }

            // the token is used only one , the the token is revoked
            // then we must generate a new token

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();

            // add the new Generated refresh token to user

            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);

            authModel.IsAuthenticated = true;
           // authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Token = user.token;
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            //var roles = await _userManager.GetRolesAsync(user);
            //authModel.Roles = roles.ToList();
            authModel.Roles = user.Role;
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
            return authModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            if (user == null)
            {
                // Handle the null user case
                throw new ArgumentNullException(nameof(user));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            //var roles = await _userManager.GetRolesAsync(user);
            //var roleClaims = new List<Claim>();

            //foreach (var role in roles)
            //    roleClaims.Add(new Claim("roles", role));
            if (user.Id == null)
            {
                // Handle the null user ID case
                throw new ArgumentNullException(nameof(user.Id));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("roles", user.Role)
            }
            .Union(userClaims);
            //.Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);
          //  var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return jwtSecurityToken;

        }

        //public async Task<bool> RevokeTokenAsync(string token)
        //{
        //    var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

        //    if (user == null)
        //        return false;

        //    var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

        //    if (!refreshToken.IsActive)
        //        return false;


        //    // kda el is active b2t = false
        //    refreshToken.RevokedOn = DateTime.UtcNow;

        //    await _userManager.UpdateAsync(user);

        //    return true;
        //}

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(50),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
