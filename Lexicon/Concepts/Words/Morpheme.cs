using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon;

namespace Lexicon
{
    /// <summary>
    /// A meaningful morphological unit of a language that cannot be further divided 
    /// </summary>
    /// <remarks>
    /// (e.g., in, come, -ing, forming incoming ).
    /// </remarks>
    public class Morpheme : IMorpheme
    {
        public IWordRoot RootWord { get; set; }
        public IDictionary<int, string> Meanings { get; set; }
        public string Origin { get; set; }
        public IDictionary<int, string> ExamplesAndDefinitions { get; set; }
    }
}
