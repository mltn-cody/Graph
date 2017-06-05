using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon.Graph
{
    public class ElasticQueryService<TStorable> : IElasticQueryService<TStorable> where TStorable : QueryStorable
    {
    }
}
