using System.Web.Http;
using Lexicon.Base.CQRS;

namespace Lexicon.WebAPI.Controllers
{
    public class WordsController : ApiController
    {
        [HttpPost, Route("words/")]
        public string AddWord(Command command)
        {
            return null;
        }

        [HttpGet, Route("words/{name}")]
        public string GetWord(string name)
        {
            return null;
        }
    }
}
