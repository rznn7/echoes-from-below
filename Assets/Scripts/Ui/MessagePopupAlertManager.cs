using UnityEngine;
using UnityEngine.UI;

public class MessagePopupAlertManager : MonoBehaviour
{
    [SerializeField]
    PopupManager popupManager;

    [SerializeField]
    Image alertImage;

    void OnEnable()
    {
        popupManager.MessageReceived += OnMessageReceived;
        popupManager.NoMoreMessageInQueue += OnNoMoreMessageInQueue;
    }

    void OnMessageReceived()
    {
        alertImage.enabled = true;
    }

    void OnNoMoreMessageInQueue()
    {
        alertImage.enabled = false;
    }
}
