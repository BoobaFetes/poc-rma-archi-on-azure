using ARO.Risk.Rma.Fopi.Application.Infrastructure;
using ARO.Risk.Rma.Fopi.Domain.Fopi;

namespace ARO.Risk.Rma.Fopi.Infra.Database.Fake
{
    public static class NimporteLaWouakDb
    {
        public static IList<FopiDetail> InMemory = new List<FopiDetail>();
        static NimporteLaWouakDb()
        {
            var random = new Random();
            for (int i = 1; i <= 10; i++)
            {
                InMemory.Add(new FopiDetail { Id = i, FuncId = 1, PayoffName = $"my fopi {i}", Context = $"context {i}", Documents = $"document {i}", Scoring = random.Next(0, 5) });
            }
        }
    }
    public class InMemoryDatabase : IInMemoryDatabase
    {
        public IEnumerable<FopiBase> All()
        {
            return NimporteLaWouakDb.InMemory.Select(i => new FopiBase
            {
                Id = i.Id,
                FuncId = i.FuncId,
                PayoffName = i.PayoffName,
                Error = i.Error,
            });
        }

        public FopiDetail? CreateFopiDetail(FopiDetail value)
        {
            NimporteLaWouakDb.InMemory.Add(value as FopiDetail);
            value.Id = NimporteLaWouakDb.InMemory.Count();
            return value;
        }

        public FopiBase? ReadFopiBase(int id)
        {
            var values = NimporteLaWouakDb.InMemory.Where(i => i.Id == id);
            if (!values.Any()) { return null; }
            return values.Select(i => new FopiBase
            {
                Id = i.Id,
                FuncId = i.FuncId,
                PayoffName = i.PayoffName,
                Error = i.Error,
            }).First();
        }

        public FopiDetail? ReadFopiDetail(int id)
        {
            return NimporteLaWouakDb.InMemory.FirstOrDefault(i => i.Id == id);
        }

        public FopiDetail? UpdateFopiDetail(int id, FopiDetail value)
        {
            int i = 0;
            for (; i < NimporteLaWouakDb.InMemory.Count; i++)
            {
                if (NimporteLaWouakDb.InMemory[i].Id == id)
                {
                    NimporteLaWouakDb.InMemory[i] = value;
                    return value;
                }
            }
            return null;
        }

        public FopiDetail? DeleteFopiDetail(int id)
        {

            int i = 0;
            for (; i < NimporteLaWouakDb.InMemory.Count; i++)
            {
                if (NimporteLaWouakDb.InMemory[i].Id == id)
                {
                    var value = NimporteLaWouakDb.InMemory[i];
                    NimporteLaWouakDb.InMemory.RemoveAt(i);
                    return value;
                }
            }
            return null;
        }
    }
}