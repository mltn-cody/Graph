using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lexicon.Base.CQRS;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.BindingGenerators;
using Ninject.Modules;
using Ninject.Syntax;

namespace Lexicon.Base.Core
{
    public class CoreNinjectModule : NinjectModule
    {
        // This is the one that grabs all of the types from each layer of the 
        // Project or Service and makes them avaiable for use.
        // Brilliant - ten points, jp does similar things in the email type with variants and such 
        // this is a common trick it seems, programming practice 
        public Assembly[] Assemblies { get; }

        public ILogger Logger => Kernel?.TryGet<ILogger>();

        public CoreNinjectModule(params Assembly[] assemblies)
        {
            Assemblies = assemblies.Union(AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("Lexicon")))
                    .ToArray();
        }

        public override void Load()
        {
            Bind<ILogger>().To<Log4NetLogger>().InSingletonScope();
            Kernel.Bind(x => x
                .From(Assemblies)
                .SelectAllTypes()
                .InheritedFrom<Aggregate>()
                .WithAttribute<AggregateAttribute>()
                .BindWith<AggregateBindingGenerator>());
        }

        public class AggregateBindingGenerator : IBindingGenerator
        {
            public IEnumerable<IBindingWhenInNamedWithOrOnSyntax<object>> CreateBindings(Type type,
                IBindingRoot bindingRoot)
            {
                return new[]
                {
                    // ToConstant does early binding on the Type defined. 
                    // Late Binding / Deferred binding until the Get of an object is done through the singletonscope
                    bindingRoot.Bind(typeof (IAggregateInformation)).ToConstant<object>(new AggregateInformation(type))
                };
            }
        }


        public void Dispose()
        {
            Logger?.Debug("Disposing the Kernel!");
            Kernel?.Dispose();
        }
    }
}
