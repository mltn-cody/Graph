namespace Lexicon.Graph.Persistance
{
    public interface IStorablePersistence
    {
        IQueryStorableRepository< T> Repository<T>() where T : Storable;
    }
}
