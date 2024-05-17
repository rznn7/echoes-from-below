using UnityEngine;

public class MapVisibility : MonoBehaviour
{
    [SerializeField]
    Canvas mapCanvas;

    public void Show()
    {
        mapCanvas.enabled = true;
    }

    public void Hide()
    {
        mapCanvas.enabled = false;
    }
}
