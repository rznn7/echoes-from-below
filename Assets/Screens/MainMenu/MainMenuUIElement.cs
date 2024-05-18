using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MainMenuUIElement : MonoBehaviour
{
    UIDocument _uiDocument;
    Button _playButton;

    [SerializeField]
    string sceneToLoad;

    void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        var root = _uiDocument.rootVisualElement;

        _playButton = root.Q<Button>("PlayButton");

        _playButton.clicked += OnPlayButtonClicked;
    }

    void OnDestroy()
    {
        _playButton.clicked -= OnPlayButtonClicked;
    }

    void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
