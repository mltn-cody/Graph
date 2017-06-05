using System.Collections.Generic;

namespace Lexicon.Base.Math.Probability
{
    public class CategoryDefinitions<T> where T : ICatagory, IList<T>
    {
        private readonly IList<T> _categoryDefinitions;

        public CategoryDefinitions()
        {
        }

        public int Length { get; private set; }
    }
}