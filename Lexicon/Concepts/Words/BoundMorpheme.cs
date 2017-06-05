using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon
{
    /// <summary>
    /// A bound morpheme is a morpheme (or word element) that cannot stand alone as a word. Contrast with free morpheme. 
    /// </summary>
    /// <remarks>
    /// Bound morphemes are also referred to as affixes. In English, bound morphemes include prefixes and suffixes.
    /// </remarks>
    public class BoundMorpheme : Morpheme, IAffix
    {
    }
}
