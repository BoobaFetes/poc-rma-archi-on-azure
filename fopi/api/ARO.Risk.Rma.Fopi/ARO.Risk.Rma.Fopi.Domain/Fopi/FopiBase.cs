using ARO.Risk.Rma.Fopi.Domain.Common.Interface;

namespace ARO.Risk.Rma.Fopi.Domain.Fopi
{
    public class FopiBase : IHaveId, IHaveFuncId, IHaveError
    {
        public int Id { get; set; }

        public int FuncId { get; set; }

        public string PayoffName { get; set; } = string.Empty;


        public Dictionary<string, ISet<string>>? Error { get; set; }
    }
}
