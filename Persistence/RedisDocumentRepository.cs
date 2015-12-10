using AnalysisModel;
using DataAccess;
using StackExchange.Redis.Extensions.Core;

namespace Persistence
{
    public class RedisDocumentRepository : RedisRepositoryAsync<Document>
    {
        public RedisDocumentRepository(ICacheClient client) : base(client)
        {

        }
    }
}
