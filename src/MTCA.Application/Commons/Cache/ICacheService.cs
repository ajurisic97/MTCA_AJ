using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Cache;
public interface ICacheService
{
    T? Get<T>(string key);
    Task<T?> GetAsync<T>(string key, CancellationToken token = default);

    void Refresh(string key);
    Task RefreshAsync(string key, CancellationToken token = default);

    void Remove(string key);
    Task RemoveAsync(string key, CancellationToken token = default);

    void Set<T>(string key, T value, TimeSpan? slidingExpiration = null);
    Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default);

    void SetAbs<T>(string key, T value, DateTimeOffset? absoluteExpiration = null);

    Task SetAbsAsync<T>(string key, T value, DateTimeOffset? absoluteExpiration = null, CancellationToken token = default);
}
