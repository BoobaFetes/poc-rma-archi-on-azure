using ARO.Risk.Rma.Fopi.Domain.Common.Interface;
using ARO.Risk.Rma.Fopi.Domain.Fopi;

namespace ARO.Risk.Rma.Fopi.Domain.Scoring
{
    public class Distance : IHaveFuncId, IHaveFopiId
    {
        public int FuncId { get; set; }

        public int FopiId { get; set; }

        public IEnumerable<FopiBase> Fopis { get; set; } = Enumerable.Empty<FopiBase>();
    }
}
