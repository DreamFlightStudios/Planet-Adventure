public interface IPolicyAgreementService
{
    bool IsAccepted { get; }

    void RequestAcceptance();
}
