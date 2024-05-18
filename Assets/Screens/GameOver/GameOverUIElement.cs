using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverUIElement : MonoBehaviour
{
    UIDocument _uiDocument;
    Button _exitButton;

    [SerializeField]
    string mainMenuScene;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        var root = _uiDocument.rootVisualElement;

        _exitButton = root.Q<Button>("ExitButton");

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
