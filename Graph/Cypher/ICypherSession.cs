using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon.Graph.Persistance;

namespace Lexicon.Graph.Cypher
{
    public interface ICypherSession
    {
         
    }

    public class Node : Storable
    {
    }

    public abstract class GraphEntity<TEntity> where TEntity : GraphEntity<TEntity>
    {

    }
}
