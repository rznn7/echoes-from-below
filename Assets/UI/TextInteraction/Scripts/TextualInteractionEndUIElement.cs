using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TextualInteractionEndUIElement : MonoBehaviour
{
    UIDocument _uiDocument;

    Label _interactionEndText;
    Button _interactionEndButton;

    public event Action EndInteraction;

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

        _interactionEndText = root.Q<Label>("interactionEndText");
        _interactionEndButton = root.Q<Button>("interactionEndButton");
    }

    void SubscribeToButtonClicks()
    {
        _interactionEndButton.clicked += OnInteractionEndButtonClicked;
    }

    void UnsubscribeFromButtonsClick()
    {
        _interactionEndButton.clicked -= OnInteractionEndButtonClicked;
    }

    void OnInteractionEndButtonClicked()
    {
        EndInteraction?.Invoke();
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
        _interactionEndText.text = interaction.InteractionEndText;
        _interactionEndButton.text = interaction.InteractionEndButtonText;
    }
}
