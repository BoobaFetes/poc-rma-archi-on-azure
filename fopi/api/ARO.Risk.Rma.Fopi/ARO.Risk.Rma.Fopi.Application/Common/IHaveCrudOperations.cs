namespace ARO.Risk.Rma.Fopi.Application.Common
{
    public interface IHaveCrudOperations<TEntity, TBase> where TEntity : TBase, new() where TBase : class, new()
    {
        IEnumerable<TBase> All();
        TBase? ReadBase(int id);

        TEntity? Create(TEntity entity);

        TEntity? Read(int id);

        TEntity? Update(int id, TEntity entity);

        TEntity? Delete(int id);
    }
}
