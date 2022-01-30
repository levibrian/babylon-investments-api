using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ivas.Transactions.Domain.Abstractions.Networking
{
    public class PolygonRoot<T> where T : class
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; }
    }
}