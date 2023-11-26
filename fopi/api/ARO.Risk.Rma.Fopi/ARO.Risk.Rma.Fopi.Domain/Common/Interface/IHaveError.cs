namespace ARO.Risk.Rma.Fopi.Domain.Common.Interface
{
    public interface IHaveError
    {
        public Dictionary<string, ISet<string>>? Error { get; set; }
    }
}
