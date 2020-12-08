

using EURIS.Service.Context;

namespace EURIS.Service.Repository
{
    public class ContextRepository<T> : BaseRepository<T>, IContextRepository<T>
        where T : class
    {
        public ContextRepository(EURISDbContext context) : base(context) { }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
