namespace ASPNETAPI.Data.Utility.Interfaces
{
    public interface IDbContextProxy
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        void AttachDbContext(object db);

        void Rollback();
    }
}