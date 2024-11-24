using ASPNETAPI.Data.Utility.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASPNETAPI.Data.Utility
{
    public class DbContextProxy : IDbContextProxy
    {
        private IList<DbContext> dbContextCollection = null;

        public DbContextProxy()
        {
            dbContextCollection = new List<DbContext>();
        }

        public void AttachDbContext(object obj)
        {
            if (obj != null)
            {
                var db = (DbContext)obj;

                if (!IsExistInCollection(db))
                {
                    dbContextCollection.Add(db);
                }
            }
        }

        private bool IsExistInCollection(DbContext db)
        {
            bool result = false;

            if (dbContextCollection.Count > 0 && dbContextCollection.IndexOf(db) >= 0)
            {
                result = true;
            }

            return result;
        }

        public void Rollback()
        {
            foreach (var db in dbContextCollection)
            {
                db.ChangeTracker.Clear();
            }
        }

        public int SaveChanges()
        {
            int affectedRowQuantity = 0;

            try
            {
                foreach (var item in dbContextCollection)
                {
                    affectedRowQuantity += item.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return affectedRowQuantity;
        }
        public async Task<int> SaveChangesAsync()
        {
            var affectedRowQuantity = 0;

            foreach (var dbContext in this.dbContextCollection)
            {
                affectedRowQuantity +=
                    await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }

            return affectedRowQuantity;
        }
    }
}