using System.Collections.Generic;

namespace Lexicon
{
    class Prefix : IPrefix
    {
        public IEnumerable<string> Meanings { get; set; }
        public string Origin { get; set; }
        public IDictionary<int, string> ExamplesAndDefinitions { get; set; }
    }
}
