using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Lexicon.Graph.Persistance
{
    public class ElasitcPersistence : IElasticPersistence
    {

        public IElasticClient Persistence { get; }
        public string Index(Type storableType)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void EnsureIndexExists(string index)
        {
            throw new NotImplementedException();
        }

        Task IPersistance.Reset()
        {
            throw new NotImplementedException();
        }

        Task IPersistance.Prepare()
        {
            throw new NotImplementedException();
        }
    }
}
