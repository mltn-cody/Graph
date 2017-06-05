using System.Runtime.InteropServices;

namespace Lexicon.Graph
{
    public interface INeo4JGraphClient
    {
        "MATCH (n) OPTIONAL MATCH(n)-[r]-() WITH n,r LIMIT 5000 DELETE n,r RETURN count(n) as deletedNodesCount"
    }
}