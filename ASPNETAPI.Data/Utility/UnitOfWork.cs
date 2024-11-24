using ASPNETAPI.Data.Utility.Interfaces;

namespace ASPNETAPI.Data.Utility
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextProxy Proxy { get; set; }

        private readonly IServiceProvider serviceProvider;

        public UnitOfWork(IDbContextProxy proxy, IServiceProvider serviceProvider)
        {
            this.Proxy = proxy;
            this.serviceProvider = serviceProvider;
        }

        public T Get<T>() where T : class
        {
            var service = serviceProvider.GetService<T>();

            if (service == null)
            {
                Type type = typeof(T);
                var genericArguments = type.GetGenericArguments().Select(x => x.Name);
                string argNames = string.Join(Environment.NewLine, genericArguments);
                throw new Exception(string.Format("找不到{0}請至Startup.cs中註冊，或請注意命名空間是否有誤。\n{1}", type.Name, argNames));
            }
            else
            {
                var getDbContextMethod = service.GetType().GetMethod("GetDbContext");
                if (getDbContextMethod != null)
                {
                    var dbContext = getDbContextMethod.Invoke(service, null);
                    Proxy.AttachDbContext(dbContext);
                }
            }
            return service;
        }

        public int Commit()
        {
            return Proxy.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await this.Proxy.SaveChangesAsync().ConfigureAwait(false);
        }

        public int CommitWithRollback()
        {
            int affectedRowQuantity = 0;

            try
            {
                Proxy.SaveChanges();
            }
            catch (Exception ex)
            {
                Proxy.Rollback();
                throw ex;
            }

            return affectedRowQuantity;
        }

        public void RollBack()
        {
            Proxy.Rollback();
        }
    }
}