using Babylon.Networking.Enums;

namespace Babylon.Networking.Constants
{
    public static class PolygonApiRoutes
    {
        private const string ApiBaseRoute = "https://api.polygon.io/v2/reference";

        private static string FinancialsApiRoute => $"{ApiBaseRoute}/financials";

        public static string GetFinancialsApiRouteByType(string ticker, FinancialsTypes type)
        {
            return $"{ FinancialsApiRoute }/{ ticker }?apiKey={ PolygonApiConfiguration.ApiKey }&type={ type }";
        }
    }
}
