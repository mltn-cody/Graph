using System.Collections.Generic;

namespace Lexicon
{
    class Suffix : ISuffix
    {
        public IEnumerable<string> Meanings { get; set; }
        public string Origin { get; set; }
        public IDictionary<int, string> ExamplesAndDefinitions { get; set; }
    }
}
