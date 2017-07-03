using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace Lexicon.Graph.Cypher
{
    public class CypherSessionFactorycs : ICypherSessionFactory
    {
        public ICypherSession Create()
        {
            throw new NotImplementedException();
        }

        public ICypherSession Create(IDriver connectionProperties)
        {
            throw new NotImplementedException();
        }

    }
}
