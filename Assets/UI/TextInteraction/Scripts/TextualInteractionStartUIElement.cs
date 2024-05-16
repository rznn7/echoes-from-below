using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TextualInteractionStartUIElement : MonoBehaviour
{
    UIDocument _uiDocument;

    Label _interactionStartText;
    Button _interactionAcceptButton;
    Button _interactionDenyButton;

    public event Action<bool> AnswerInteraction;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        InitUIElements();
        SubscribeToButtonClicks();
    }

    void OnDestroy()
    {
        UnsubscribeFromButtonsClick();
    }

    void InitUIElements()
    {
        var root = _uiDocument.rootVisualElement;

        _interactionStartText = root.Q<Label>("interactionStartText");
        _interactionAcceptButton = root.Q<Button>("interactionAcceptButton");
        _interactionDenyButton = root.Q<Button>("interactionDenyButton");
    }

    void SubscribeToButtonClicks()
    {
        _interactionAcceptButton.clicked += OnInteractionAcceptButtonClicked;
        _interactionDenyButton.clicked += OnInteractionDenyButtonClicked;
    }

    void UnsubscribeFromButtonsClick()
    {
        _interactionAcceptButton.clicked -= OnInteractionAcceptButtonClicked;
        _interactionDenyButton.clicked -= OnInteractionDenyButtonClicked;
    }

    void OnInteractionDenyButtonClicked()
    {
        AnswerInteraction?.Invoke(false);
    }

    void OnInteractionAcceptButtonClicked()
    {
        AnswerInteraction?.Invoke(true);
    }

    public void Show()
    {
        _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void SetData(TextualInteraction interaction)
    {
        _interactionStartText.text = interaction.InteractionStartText;
        _interactionAcceptButton.text = interaction.InteractionAcceptButtonText;
        _interactionDenyButton.text = interaction.InteractionDenyButtonText;
    }
}
