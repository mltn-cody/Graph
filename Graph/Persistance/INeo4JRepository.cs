using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Castle.Core.Logging;

namespace Lexicon.Graph
{
    public class Neo4JRepository<TStorable> : IQueryStorableRepository<TStorable>
    {
        private readonly ILogger _logger;
        private readonly INeo4JPersistence _neo4;
        private string Index; // maybe unnecessary 
        private int _retryAttempts;
        private readonly int _retryDelay;
        private readonly bool _throwFailedExceptions;

        public async Task<TStorable> GetAsync(string key)
        {
            var storable = await GetOrNullAsync(key).ConfigureAwait(false);
            if (storable == null)
            {
                throw new Exception($"Type not found.");
            }
            return storable;
        }

        public async Task<TStorable> GetOrNullAsync(string key)
        {
            throw new NotImplementedException();
        }


        public void Remove()
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new System.NotImplementedException();
        }
    }
}