using ARO.Risk.Rma.Fopi.Application.Common;
using ARO.Risk.Rma.Fopi.Application.Infrastructure;
using ARO.Risk.Rma.Fopi.Domain.Fopi;

namespace ARO.Risk.Rma.Fopi.Application.Fopi
{
    public class FopiUsecases : IFopiUsecases
    {
        private readonly IInMemoryDatabase inMemoryDatabase;

        public FopiUsecases(IInMemoryDatabase inMemoryDatabase)
        {
            this.inMemoryDatabase = inMemoryDatabase;
        }

        public IEnumerable<FopiBase> All()
        {
            return this.inMemoryDatabase.All();
        }   

        public FopiBase? ReadBase(int id)
        {
            return this.inMemoryDatabase.ReadFopiBase(id);
        }

        public FopiDetail? Read(int id)
        {
            return this.inMemoryDatabase.ReadFopiDetail(id);
        }

        public FopiDetail? Create(FopiDetail entity)
        {
            return this.inMemoryDatabase.CreateFopiDetail(entity);
        }

        public FopiDetail? Delete(int id)
        {
            return this.inMemoryDatabase.DeleteFopiDetail(id);
        }

        public FopiDetail? Update(int id, FopiDetail entity)
        {
            return this.inMemoryDatabase.UpdateFopiDetail(id, entity);
        }
    }
}
