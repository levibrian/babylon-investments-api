namespace Ivas.Transactions.Api.Constants
{
    public static class IvasApiHeaders
    {
        // Request Headers
        public const string RapidApiUserKey = "X-RapidAPI-User";
        public const string RapidApiKey = "X-RapidAPI-Key";
        public const string AwsApiKey = "X-Api-Key";
        
        // Response Headers
        public const string UnAuthorizedHeader = "Not Authorized";
        public const string UnAuthorizedErrorMessage = "RapidApi User or RapidApi Key not specified";
    }
}