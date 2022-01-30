using System.Collections.Generic;
using System.Threading.Tasks;

namespace Babylon.Networking.Base.Interfaces
{
    public interface IPolygonBroker
    {
        Task<IEnumerable<T>> Get<T>(string api) where T : class;
    }
}
