﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Babylon.Investments.Domain.Abstractions.Brokers.Base
{
    public class PolygonRoot<T> where T : class
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; }
    }
}