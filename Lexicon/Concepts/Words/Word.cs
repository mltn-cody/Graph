using System.Collections.Generic;

namespace Lexicon
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class Word : FreeMorpheme
    {
        public Word(dynamic properties)
        {
        }

        public List<FreeMorpheme> WordParts { get; protected set; }
        public List<BoundMorpheme> AffixParts { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWordRoot GetWordRoot()
        {
            return null;
        }
    }
}


// One Thing At A Time.

    // Get Test Project running so you can get some output
    // Get The Cypher Query abstration working 