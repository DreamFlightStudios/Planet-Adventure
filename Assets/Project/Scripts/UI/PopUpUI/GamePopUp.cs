using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePopUp : UIScreen
{
    [Header("Content")]
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private GameObject _moreInfo;
    [SerializeField] private TMP_Text _moreInfoText;
    [SerializeField] private Image _image;

    [Header("Buttons")]
    [SerializeField] private Button _acceptButton;
    [SerializeField] private TMP_Text _acceptLabel;
    [SerializeField] private Button _discardButton;
    [SerializeField] private TMP_Text _discardLabel;
    [SerializeField] private Button _okButton;
    [SerializeField] private TMP_Text _okLabel;

    private PopUpConfig _config;

    public event Action<PopUpResult> ResultSelected;
    public event Action Hidden;

    public override bool CanCloseByTrigger => _config == null || _config.IsClosableByEscape;

    public void Setup(PopUpConfig config)
    {
        _config = config;

        SetupText(_title, config.IsTitleActive, config.Title);
        SetupText(_description, config.IsDescriptionActive, config.Description);

        _moreInfo.SetActive(config.IsMoreInfoActive);
        _moreInfoText.text = config.MoreInfo;

        bool isImageActive = config.IsImageActive && config.Image != null;
        _image.gameObject.SetActive(isImageActive);
        _image.sprite = config.Image;

        SetupButton(_acceptButton, _acceptLabel, config.AcceptButton);
        SetupButton(_discardButton, _discardLabel, config.DiscardButton);
        SetupButton(_okButton, _okLabel, config.OkButton);
    }

    protected override void OnCreate()
    {
        _acceptButton.onClick.AddListener(OnAcceptButtonClicked);
        _discardButton.onClick.AddListener(OnDiscardButtonClicked);
        _okButton.onClick.AddListener(OnOkButtonClicked);

        SwitchState(false);
    }

    protected override void OnHide()
        => Hidden?.Invoke();

    protected override void OnDispose()
    {
        _acceptButton.onClick.RemoveListener(OnAcceptButtonClicked);
        _discardButton.onClick.RemoveListener(OnDiscardButtonClicked);
        _okButton.onClick.RemoveListener(OnOkButtonClicked);
    }

    private void SetupText(TMP_Text text, bool isActive, string value)
    {
        text.gameObject.SetActive(isActive);
        text.text = value;
    }

    private void SetupButton(Button button, TMP_Text label, PopUpButtonInfo info)
    {
        bool isActive = info != null && info.IsActive;
        button.gameObject.SetActive(isActive);

        if (isActive && string.IsNullOrEmpty(info.Label) == false)
            label.text = info.Label;
    }

    private void OnAcceptButtonClicked()
        => ResultSelected?.Invoke(PopUpResult.Accepted);

    private void OnDiscardButtonClicked()
        => ResultSelected?.Invoke(PopUpResult.Discarded);

    private void OnOkButtonClicked()
        => ResultSelected?.Invoke(PopUpResult.Closed);
}
