using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameWinTrigger : MonoBehaviour, IHandleInteraction
{
    [SerializeField]
    float fadeDuration = 4f;

    public void TriggerGameWin()
    {
        StartCoroutine(FadeOutAndLoadSceneCoroutine());
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

        SceneManager.LoadScene("GameWin");

        yield return new WaitForSeconds(0.1f);

        Destroy(canvasObject);
    }

    public IEnumerator HandleEventInteraction()
    {
        TriggerGameWin();
        yield break;
    }
}
