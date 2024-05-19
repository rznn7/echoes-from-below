using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameOverTrigger : MonoBehaviour
{
    [FormerlySerializedAs("duration")]
    [SerializeField]
    float fadeDuration = 4f;

    public void TriggerGameOver(GameOverType gameOverType)
    {
        GameOverMessage.Instance.message = GetGameOverMessage(gameOverType);
        StartCoroutine(FadeOutAndLoadSceneCoroutine());
    }

    static string GetGameOverMessage(GameOverType gameOverType)
    {
        return gameOverType switch
        {
            GameOverType.Leakage =>
                "Despite your efforts, the leaks proved too much to handle. Your submarine is now descending into the abyss.",
            GameOverType.OutOfPower =>
                "You ran out of power and your submarine is now drifting aimlessly into the depths.",
            GameOverType.OutOfOxygen =>
                "Oxygen levels hit zero and your submarine is now a lifeless shell sinking into the abyss.",
            _ => "Something went wrong, and now your submarine is heading towards the abyss."
        };
    }

    IEnumerator FadeOutAndLoadSceneCoroutine()
    {
        var timeElapsed = 0f;

        var canvasObject = new GameObject("FadeCanvas");
        var canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObject.AddComponent<GraphicRaycaster>();

        var imageObject = new GameObject("FadeImage");
        imageObject.transform.SetParent(canvasObject.transform, false);
        var fadeImage = imageObject.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // Couleur noire avec alpha initial à 0

        var rectTransform = fadeImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            var alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        SceneManager.LoadScene("Scenes/ProductionScenes/GameOver");

        yield return new WaitForSeconds(0.1f);

        Destroy(canvasObject);
    }
}

public enum GameOverType
{
    Leakage,
    OutOfPower,
    OutOfOxygen
}
