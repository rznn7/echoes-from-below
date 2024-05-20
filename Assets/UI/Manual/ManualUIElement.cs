using UnityEngine;
using UnityEngine.UIElements;

public class ManualUIElement : MonoBehaviour
{
    UIDocument _uiDocument;

    Button _closeButton;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        InitUIElements();
        SubscribeToButtonClick();
        HideManual();
    }

    void OnDestroy()
    {
        UnsubscribeFromButtonClick();
    }

    void InitUIElements()
    {
        var root = _uiDocument.rootVisualElement;

        _closeButton = root.Q<Button>("CloseButton");
    }

    void SubscribeToButtonClick()
    {
        _closeButton.clicked += OnCloseButtonClicked;
    }

    void UnsubscribeFromButtonClick()
    {
        _closeButton.clicked -= OnCloseButtonClicked;
    }

    void OnCloseButtonClicked()
    {
        HideManual();
    }

    public void ShowManual()
    {
        _uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    void HideManual()
    {
        _uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
}
