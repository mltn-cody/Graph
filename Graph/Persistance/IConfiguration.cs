using System.Collections.Generic;

namespace Lexicon.Graph.Persistance
{
    public interface IConfiguration : IDictionary<string, object>
    {
        T GetConfigValueAs<T>(string name);

        T GetConfigValueOrDefault<T>(string name, T defaultValueIfNotFound);
    }
}