using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupUIElement : MonoBehaviour
{
    UIDocument _uiDocument;

    Label _popupText;
    Button _popupButton;

    public event Action PopupClosed;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        InitUIElements();
        SubscribeToButtonClick();
        HidePopup();
    }

    void OnDestroy()
    {
        UnsubscribeFromButtonClick();
    }

    void InitUIElements()
    {
        var root = _uiDocument.rootVisualElement;

        _popupText = root.Q<Label>("popupText");
        _popupButton = root.Q<Button>("popupButton");
    }

    void SubscribeToButtonClick()
    {
        _popupButton.clicked += OnPopupButtonClicked;
    }

    void UnsubscribeFromButtonClick()
    {
        _popupButton.clicked -= OnPopupButtonClicked;
    }

    void OnPopupButtonClicked()
    {
        HidePopup();
        PopupClosed?.Invoke();
    }

    public void SetPopupData(PopupMessage popupMessage)
    {
        ShowPopup();
        _popupText.text = popupMessage.MessageText;
        _popupButton.text = popupMessage.ButtonText;
    }

    void ShowPopup()
    {
        _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        // _uiDocument.sortingOrder = 10;
    }

    void HidePopup()
    {
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
        // _uiDocument.sortingOrder = -10;
    }
}
