using System;
using Babylon.Transactions.Api.Constants;
using Babylon.Transactions.Domain.Constants;
using Babylon.Transactions.Domain.Cryptography;
using Babylon.Transactions.Shared.Exceptions.Custom;
using Microsoft.AspNetCore.Mvc;

namespace Babylon.Transactions.Api.Controllers.Base
{
    [Controller]
    public class BabylonController : ControllerBase
    {
        protected string ClientIdentifier => HashClientIdentifier();

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
        
        private string HashClientIdentifier()
        {
            var unhashedClientIdentifier =
                !string.IsNullOrEmpty(RapidApiUser) && !string.IsNullOrEmpty(RapidApiKey)
                    ? $"{RapidApiUser}-{RapidApiKey}"
                    : !string.IsNullOrEmpty(OverrideApiKey)
                        ? $"{OverrideApiKey}"
                        : throw new BabylonException(BabylonApiHeaders.UnAuthorizedErrorMessage); 

            var ivArray = Convert.FromBase64String(CipherVariables.Iv);
            
            var hashedClientIdentifier = _aesCipher
                .Encrypt(unhashedClientIdentifier, ivArray);
            
            return hashedClientIdentifier;
        }
    }
}