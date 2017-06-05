using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Lexicon.Graph
{
    public interface INeo4JPersistence : IPersistance
    {
        INeo4JGraphClient Persistence { get; }
        ISession CreateSession();
        ITransaction CreateTransaction();
        void EnsureLabelExists<TStorable>() where TStorable : QueryStorable;
        Task EnsureLabelExistsAsync<TStorable>() where TStorable : QueryStorable;
    }

    //OBject relational mappings lets get back to that for minute, that's the enitty framework key you don't want to have that kind of storage. Go Neo4j Directly 
}