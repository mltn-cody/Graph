using System;
using Lexicon.Graph.Persistance;
using Ninject.Modules;

namespace Lexicon.Graph.Core
{
    class GraphNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPersistance,INeo4JPersistence>().To<Neo4JPersistence>().InSingletonScope();
            Bind(typeof (IElasticQueryService<>)).To(typeof (ElasticQueryService<>));

        }
    }
}
