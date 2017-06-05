using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lexicon.Base.Core
{
    public interface IAggregateInformation
    {
        Type Type { get; }
        string TypeName { get; }
        string TypeFullName { get; }
        Assembly AssemblyName { get; }
        bool IsSingleton { get; }
        bool AllowNonSingletonEmptyGuidId { get; set; }
        IReadOnlyList<string>  Commands { get; }
    }
}