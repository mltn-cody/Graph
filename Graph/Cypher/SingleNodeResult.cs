namespace Lexicon.Graph.Cypher
{
    internal class SingleNodeResult
    {
        public SingleNodeResult(Node newNode)
        {
            NewNode = newNode;
        }

        public Node NewNode { get; private set; }
    }
}