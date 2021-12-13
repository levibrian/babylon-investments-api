using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Ivas.Transactions.Api.Constants;
using Ivas.Transactions.Domain.Cryptography;
using Ivas.Transactions.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ivas.Transactions.Api.Controllers.Base
{
    [Controller]
    public class IvasController : ControllerBase
    {
        protected string RapidApiUser => GetRapidApiUserFromHeaders();

        protected string RapidApiKey => GetRapidApiKeyFromHeaders();
        
        protected string ClientIdentifier => HashClientIdentifier();

        protected readonly IAesCipher AesCipher;
        
        public IvasController(IAesCipher aesCipher)
        {
            AesCipher = aesCipher ?? throw new ArgumentNullException(nameof(aesCipher));
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
            var unhashedClientIdentifier = $"{RapidApiUser}-{RapidApiKey}";

            using var aesManaged = new AesManaged();
            
            aesManaged.GenerateIV();
            
            var hashedClientIdentifier = AesCipher
                .EncryptStringToBytes(unhashedClientIdentifier, aesManaged.IV)
                .FromByteArrayToString();
            
            return hashedClientIdentifier;
        }
    }
}