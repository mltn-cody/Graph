using Neo4j.Driver.V1;

namespace Lexicon.Graph.Cypher
{
    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// Remarks on session vs transaction <see cref="http://stackoverflow.com/questions/39525713/session-run-vs-transaction-run-in-neo4j-bolt"/>
    /// </remarks>
    public interface ICypherSessionFactory
    {
        ICypherSession Create();
        ICypherSession Create(IDriver connectionProperties);

    }
}
