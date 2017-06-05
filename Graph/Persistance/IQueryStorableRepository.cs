namespace Lexicon.Graph
{
    /// <summary>you
    /// 
    /// </summary>
    /// <remarks>Is the Neo4j Repository a Query Storable, no the Neo4j is your permanent storage and again elastic search is your queryable</remarks>
    /// <remarks>Neo4j will apply a filter to your searches though so it's queryable too. After the initial Elastic search according to documents</remarks>
    /// <remarks></remarks>
    /// <typeparam name="TStorable"></typeparam>
    public interface IQueryStorableRepository<TStorable>
    {
        void Remove();

        void Refresh();

        void RemoveAll();
    }
}