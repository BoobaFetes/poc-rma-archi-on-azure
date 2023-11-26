namespace ARO.Risk.Rma.Fopi.Domain.Common
{
    [Flags]
    public enum ErrorCode
    {
        Unkow = 0x00000000,

        DomainLayer = 0x00010000,
        ApplicationLayer = 0x00100000,
        InfraLayer = 0x00110000,
        ApiLayer = 0x01000000,


        NotSet = 0x00000001,
        IdShouldBeSet = 0x00000010,
        IdShouldNotBeSet = 0x00000011,
        NotLinked = 0x00000100,
    }
}
