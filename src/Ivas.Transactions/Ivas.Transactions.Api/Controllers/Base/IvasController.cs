using System;
using Ivas.Transactions.Api.Constants;
using Ivas.Transactions.Domain.Constants;
using Ivas.Transactions.Domain.Cryptography;
using Ivas.Transactions.Shared.Exceptions.Custom;
using Microsoft.AspNetCore.Mvc;

namespace Ivas.Transactions.Api.Controllers.Base
{
    [Controller]
    public class IvasController : ControllerBase
    {
        protected string ClientIdentifier => HashClientIdentifier();

        private string RapidApiUser => GetRapidApiUserFromHeaders();
     
        private string RapidApiKey => GetRapidApiKeyFromHeaders();

        private string OverrideApiKey => GetOverrideApiKeyValueFromHeaders();
        
        private readonly IAesCipher _aesCipher;
        
        public IvasController(IAesCipher aesCipher)
        {
            _aesCipher = aesCipher ?? throw new ArgumentNullException(nameof(aesCipher));
        }

        private string GetOverrideApiKeyValueFromHeaders()
        {
            Request.Headers.TryGetValue(IvasApiHeaders.OverrideApiKey, out var overrideApiKeyValue);

            return overrideApiKeyValue;
        }
        
        private string GetRapidApiUserFromHeaders()
        {
            Request.Headers.TryGetValue(IvasApiHeaders.RapidApiUserKey, out var rapidApiUser);

            return rapidApiUser;
        }

        private string GetRapidApiKeyFromHeaders()
        {
            Request.Headers.TryGetValue(IvasApiHeaders.RapidApiKey, out var rapidApiKey);

            return rapidApiKey;
        }
        
        private string HashClientIdentifier()
        {
            var unhashedClientIdentifier =
                !string.IsNullOrEmpty(RapidApiUser) && !string.IsNullOrEmpty(RapidApiKey)
                    ? $"{RapidApiUser}-{RapidApiKey}"
                    : !string.IsNullOrEmpty(OverrideApiKey)
                        ? $"{OverrideApiKey}"
                        : throw new IvasException(IvasApiHeaders.UnAuthorizedErrorMessage); 

            var ivArray = Convert.FromBase64String(CipherVariables.Iv);
            
            var hashedClientIdentifier = _aesCipher
                .Encrypt(unhashedClientIdentifier, ivArray);
            
            return hashedClientIdentifier;
        }
    }
}