using System;
using Babylon.Investments.Api.Constants;
using Babylon.Investments.Domain.Constants;
using Babylon.Investments.Domain.Cryptography;
using Babylon.Investments.Shared.Exceptions.Custom;
using Microsoft.AspNetCore.Mvc;

namespace Babylon.Investments.Api.Controllers.Base
{
    [Controller]
    public class BabylonController : ControllerBase
    {
        protected string TenantIdentifier => HashTenantIdentifier();

        private string RapidApiUser => GetRapidApiUserFromHeaders();
     
        private string RapidApiKey => GetRapidApiKeyFromHeaders();

        private string OverrideApiKey => GetOverrideApiKeyValueFromHeaders();
        
        private readonly IAesCipher _aesCipher;
        
        public BabylonController(IAesCipher aesCipher)
        {
            _aesCipher = aesCipher ?? throw new ArgumentNullException(nameof(aesCipher));
        }

        private string GetOverrideApiKeyValueFromHeaders()
        {
            Request.Headers.TryGetValue(BabylonApiHeaders.OverrideApiKey, out var overrideApiKeyValue);

            return overrideApiKeyValue;
        }
        
        private string GetRapidApiUserFromHeaders()
        {
            Request.Headers.TryGetValue(BabylonApiHeaders.RapidApiUserKey, out var rapidApiUser);

            return rapidApiUser;
        }

        private string GetRapidApiKeyFromHeaders()
        {
            Request.Headers.TryGetValue(BabylonApiHeaders.RapidApiKey, out var rapidApiKey);

            return rapidApiKey;
        }
        
        private string HashTenantIdentifier()
        {
            var unhashedTenantIdentifier =
                !string.IsNullOrEmpty(RapidApiUser) && !string.IsNullOrEmpty(RapidApiKey)
                    ? $"{RapidApiUser}-{RapidApiKey}"
                    : !string.IsNullOrEmpty(OverrideApiKey)
                        ? $"{OverrideApiKey}"
                        : throw new BabylonException(BabylonApiHeaders.UnAuthorizedErrorMessage); 

            var ivArray = Convert.FromBase64String(CipherVariables.Iv);
            
            var hashedTenantIdentifier = _aesCipher
                .Encrypt(unhashedTenantIdentifier, ivArray);
            
            return hashedTenantIdentifier;
        }
    }
}