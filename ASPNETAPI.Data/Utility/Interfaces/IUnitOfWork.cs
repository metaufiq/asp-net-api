namespace ASPNETAPI.Data.Utility.Interfaces
{
    public interface IUnitOfWork
    {
        T Get<T>() where T : class;

        int Commit();

        Task<int> CommitAsync();

        int CommitWithRollback();

        void RollBack();
    }
}