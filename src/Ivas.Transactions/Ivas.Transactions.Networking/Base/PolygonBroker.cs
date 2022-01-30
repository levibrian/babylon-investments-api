using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Ivas.Transactions.Domain.Abstractions.Networking;
using Ivas.Transactions.Networking.Base.Interfaces;

namespace Ivas.Transactions.Networking.Base
{
    public class PolygonBroker : IPolygonBroker
    {
        public async Task<IEnumerable<T>> Get<T>(string api) where T : class
        {
            try
            {
                using var client = new HttpClient();

                var response = await client.GetAsync(api);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Call returned a failed Response");
                }

                var result = await response.Content.ReadAsStringAsync();
                
                var polygonResponse = JsonSerializer.Deserialize<PolygonRoot<T>>(result);

                if (polygonResponse != null && !polygonResponse.Status.Equals("OK"))
                {
                    throw new Exception("Response from the Polygon API is not 'OK'");
                }

                return polygonResponse.Results;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
