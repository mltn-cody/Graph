using System;
using Nest;

namespace Lexicon.Graph
{
    public interface IElasticPersistence : IPersistance
    {
        IElasticClient Persistence { get; }
        string Index(Type storableType);
        void Connect();

        void EnsureIndexExists(string index);
    }
}