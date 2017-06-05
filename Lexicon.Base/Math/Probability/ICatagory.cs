using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Lexicon.Base.Math.Probability
{
    public interface ICatagory
    {
        string Name { get; set; }
        bool CatagoryDefinition();
    }
}