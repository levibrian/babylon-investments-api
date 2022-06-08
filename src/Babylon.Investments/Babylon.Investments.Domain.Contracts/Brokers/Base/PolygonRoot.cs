using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Babylon.Investments.Domain.Contracts.Brokers.Base
{
    public class PolygonRoot<T> where T : class
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; }
    }
}