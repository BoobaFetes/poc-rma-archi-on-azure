using ARO.Risk.Rma.Fopi.Domain.Fopi;

namespace ARO.Risk.Rma.Fopi.Application.Infrastructure
{
    public interface IInMemoryDatabase
    {
        IEnumerable<FopiBase> All();

        FopiBase? ReadFopiBase(int id);

        FopiDetail? CreateFopiDetail(FopiDetail value);

        FopiDetail? ReadFopiDetail(int id);

        FopiDetail? UpdateFopiDetail(int id, FopiDetail value);

        FopiDetail? DeleteFopiDetail(int id);
    }
}
