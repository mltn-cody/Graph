namespace Lexicon.Graph.Cypher
{
    public class CypherSession : ICypherSession
    {
        private const string CreateConstraintClauseFormat = "";
        private const string DropConstraintClauseFormat = "";

        private const string CreateIndexClauseFormat = "";
        private const string DropIndexClauseFormat = "";

        private static readonly int[] MinimumVersionNumber = new[] { 2, 0, 0 };

        private static readonly string NodeVariableName = "";

        private static readonly string CreateNodeClauseFormat =
            string.Format(@"CREATE ({0}{{0}} {{1}}) RETURN {0} as {{2}}, id({0}) as {{3}}, labels({0}) as {{4}};",
                NodeVariableName);

        private readonly string _uri;

        internal CypherSession()
        {

        }
    }
}