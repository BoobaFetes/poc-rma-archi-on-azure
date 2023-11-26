using ARO.Risk.Rma.Fopi.Domain.Common.Interface;

namespace ARO.Risk.Rma.Fopi.Domain.Fopi
{
    public class FopiDetail : FopiBase
    {
        public int? Scoring { get; set; }

        public string Context { get; set; } = "not implemented yet";

        public string Documents { get; set; } = "not implemented yet";
    }
}
