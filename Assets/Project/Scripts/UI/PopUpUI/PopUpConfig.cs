using UnityEngine;

[CreateAssetMenu(fileName = "New PopUp Config", menuName = "Configs/UI/Create PopUp Config")]
public class PopUpConfig : ScriptableObject
{
    [Header("Title")]
    [field: SerializeField] public bool IsTitleActive { get; private set; } = true;
    [field: SerializeField] public string Title { get; private set; }

    [Header("Description")]
    [field: SerializeField] public bool IsDescriptionActive { get; private set; } = true;
    [field: SerializeField, TextArea(2, 6)] public string Description { get; private set; }

    [Header("More Info")]
    [field: SerializeField] public bool IsMoreInfoActive { get; private set; }
    [field: SerializeField, TextArea(5, 20)] public string MoreInfo { get; private set; }

    [Header("Image")]
    [field: SerializeField] public bool IsImageActive { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }

    [Header("Buttons")]
    [field: SerializeField] public PopUpButtonInfo AcceptButton { get; private set; }
    [field: SerializeField] public PopUpButtonInfo DiscardButton { get; private set; }
    [field: SerializeField] public PopUpButtonInfo OkButton { get; private set; }

    [Header("Behaviour")]
    [field: SerializeField] public bool IsClosableByEscape { get; private set; } = true;

    private void OnValidate()
    {
        bool hasActiveButton = AcceptButton is { IsActive: true } || DiscardButton is { IsActive: true } || OkButton is { IsActive: true };

        if (hasActiveButton == false && IsClosableByEscape == false)
            Debug.LogWarning($"PopUp config '{name}' has no active buttons and cannot be closed by Escape. The popup will never close.", this);
    }
}
