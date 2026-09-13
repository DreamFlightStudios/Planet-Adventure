using UnityEngine;

public class PolicyAgreementService : IPolicyAgreementService
{
    private readonly ISaveRepository<PolicyAgreementData> _repository;
    private readonly PolicyConfig _configuration;
    private readonly IPopUpService _popUpService;

    public bool IsAccepted => _configuration == null || _repository.Data.AcceptedPolicyVersion >= _configuration.Version;

    public PolicyAgreementService(ISaveRepository<PolicyAgreementData> repository, PolicyConfig configuration, IPopUpService popUpService)
    {
        _repository = repository;
        _configuration = configuration;
        _popUpService = popUpService;
    }

    public void Initialize()
    {
        _repository.Load();

        if (_configuration == null)
            Debug.LogError($"{nameof(PolicyConfig)} is not assigned. Policy agreement is skipped.");
    }

    public void RequestAcceptance()
    {
        if (IsAccepted)
            return;

        _popUpService.Show(_configuration.PopUp, OnPolicyClosed);
    }

    private void OnPolicyClosed(PopUpResult result)
    {
        if (result != PopUpResult.Accepted)
        {
            QuitGame();
            return;
        }

        _repository.Data.AcceptedPolicyVersion = _configuration.Version;
        _repository.Save();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
