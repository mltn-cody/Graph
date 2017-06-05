using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon.Graph
{
    /// <summary>
    /// The idea here is to use a simple storage like entity framework and 
    /// Then advance to what you want to use your graph database
    /// </summary>
    public interface IPersistance
    {
        /// <summary>
        /// Resets the persistence layer(s).
        /// </summary>
        Task Reset();

        /// <summary>
        /// Prepares the persistence layer(s).
        /// </summary>
        Task Prepare();
    }
}
