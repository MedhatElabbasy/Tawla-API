using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NETCore.Encrypt;
using Tawala.Domain.Entities.Identity;

namespace Tawala.Application.Services.UsersService
{
 
    public class AppUserManager : UserManager<AppUser>
    {
        private readonly IConfiguration _configuration;

        public AppUserManager(IUserStore<AppUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher, IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<AppUser>> logger,
            IConfiguration configuration)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators,
                keyNormalizer, errors, services, logger)
        {
            _configuration = configuration;
        }

        #region Authenticator App key

        public override string GenerateNewAuthenticatorKey()
        {
            var originalAuthenticatorKey = base.GenerateNewAuthenticatorKey();

            // var aesKey = EncryptProvider.CreateAesKey();

            bool.TryParse(_configuration["TwoFactorAuthentication:EncryptionEnabled"], out bool encryptionEnabled);

            var encryptedKey = encryptionEnabled
                ? EncryptProvider.AESEncrypt(originalAuthenticatorKey, _configuration["TwoFactorAuthentication:EncryptionKey"])
                : originalAuthenticatorKey;

            return encryptedKey;
        }

        public override async Task<string> GetAuthenticatorKeyAsync(AppUser user)
        {
            var databaseKey = await base.GetAuthenticatorKeyAsync(user);

            if (databaseKey == null)
            {
                return null;
            }

            // Decryption
            bool.TryParse(_configuration["TwoFactorAuthentication:EncryptionEnabled"], out bool encryptionEnabled);

            var originalAuthenticatorKey = encryptionEnabled
                ? EncryptProvider.AESDecrypt(databaseKey, _configuration["TwoFactorAuthentication:EncryptionKey"])
                : databaseKey;

            return originalAuthenticatorKey;
        }

        #endregion

        #region Recovery codes

        protected override string CreateTwoFactorRecoveryCode()
        {
            var originalRecoveryCode = base.CreateTwoFactorRecoveryCode();

            bool.TryParse(_configuration["TwoFactorAuthentication:EncryptionEnabled"], out bool encryptionEnabled);

            var encryptedRecoveryCode = encryptionEnabled
                ? EncryptProvider.AESEncrypt(originalRecoveryCode, _configuration["TwoFactorAuthentication:EncryptionKey"])
                : originalRecoveryCode;

            return encryptedRecoveryCode;
        }

        public override async Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(AppUser user, int number)
        {
            var tokens = await base.GenerateNewTwoFactorRecoveryCodesAsync(user, number);

            var generatedTokens = tokens as string[] ?? tokens.ToArray();
            if (!generatedTokens.Any())
            {
                return generatedTokens;
            }

            bool.TryParse(_configuration["TwoFactorAuthentication:EncryptionEnabled"], out bool encryptionEnabled);

            return encryptionEnabled
                ? generatedTokens
                    .Select(token =>
                        EncryptProvider.AESDecrypt(token, _configuration["TwoFactorAuthentication:EncryptionKey"]))
                : generatedTokens;

        }

        public override Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(AppUser user, string code)
        {
            bool.TryParse(_configuration["TwoFactorAuthentication:EncryptionEnabled"], out bool encryptionEnabled);

            if (encryptionEnabled && !string.IsNullOrEmpty(code))
            {
                code = EncryptProvider.AESEncrypt(code, _configuration["TwoFactorAuthentication:EncryptionKey"]);
            }

            return base.RedeemTwoFactorRecoveryCodeAsync(user, code);
        }

        #endregion

    }
}
