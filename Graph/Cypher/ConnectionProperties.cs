using Neo4j.Driver.V1;

namespace Lexicon.Graph.Cypher
{
    public class ConnectionProperties
    {
        private Config _neo4jConfig;

        public ConnectionProperties(EncryptionLevel encryptionLevel)
        {
            _neo4jConfig = Config.DefaultConfig;
        }
    }
}