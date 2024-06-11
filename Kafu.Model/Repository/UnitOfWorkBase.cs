using Kafu.Model.Entities;


namespace Kafu.Model.Repository
{
    public class UnitOfWorkBase : IDisposable
    {
        public Kafu_SystemContext context;

        public UnitOfWorkBase(Kafu_SystemContext cont)
        {
            context = cont;
        }

        public RepositoryBase<Employee> EmployeeRepositoryBase => new RepositoryBase<Employee>(context);

        public RepositoryBase<EmployeImg> EmployeeImgRepositoryBase => new RepositoryBase<EmployeImg>(context);
        public RepositoryBase<KafuRecord> KafuRecordRepositoryBase => new RepositoryBase<KafuRecord>(context);
        public RepositoryBase<ReasonLookup> ReasonepositoryBase => new RepositoryBase<ReasonLookup>(context);
        public RepositoryBase<EmployeDetails> EmployeeDetailsRepositoryBase => new RepositoryBase<EmployeDetails>(context);
       
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
