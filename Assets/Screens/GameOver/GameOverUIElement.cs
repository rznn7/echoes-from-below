using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class GameOverUIElement : MonoBehaviour
{
    UIDocument _uiDocument;
    Button _exitButton;
    Label _gameOverText;

    [SerializeField]
    string mainMenuScene;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        var root = _uiDocument.rootVisualElement;

        _exitButton = root.Q<Button>("ExitButton");
        _gameOverText = root.Q<Label>("GameOverText");

        _gameOverText.text = GameOverMessage.Instance.message;
        _exitButton.clicked += OnExitButtonClicked;
    }

    void OnDestroy()
    {
        _exitButton.clicked -= OnExitButtonClicked;
    }

    void OnExitButtonClicked()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
