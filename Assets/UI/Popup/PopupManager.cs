using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    PopupUIElement _popupUIElement;
    readonly Queue<PopupMessage> _messagesQueue = new();

    void Awake()
    {
        _popupUIElement = GetComponent<PopupUIElement>();
        _popupUIElement.PopupClosed += OnPopupClosed;
    }

    void Start()
    {
        SubscribeToMessageSenders();
        SubscribeToPopupActivators();
    }

    void OnDestroy()
    {
        UnsubscribeFromSubscribeToMessageSenders();
        UnsubscribeFromPopupActivators();
    }

    void SubscribeToMessageSenders()
    {
        FindObjectsOfType<MonoBehaviour>().OfType<IPopupMessageSender>().ToList()
            .ForEach(sender => sender.AddMessagesToQueue += OnMessagesReceived);
    }

    void SubscribeToPopupActivators()
    {
        FindObjectsOfType<MonoBehaviour>().OfType<IPopupActivator>().ToList()
            .ForEach(sender => sender.ActivatePopup += OnPopupActivated);
    }

    void UnsubscribeFromSubscribeToMessageSenders()
    {
        FindObjectsOfType<MonoBehaviour>().OfType<IPopupMessageSender>().ToList()
            .ForEach(sender => sender.AddMessagesToQueue -= OnMessagesReceived);
    }

    void UnsubscribeFromPopupActivators()
    {
        FindObjectsOfType<MonoBehaviour>().OfType<IPopupActivator>().ToList()
            .ForEach(sender => sender.ActivatePopup -= OnPopupActivated);
    }

    void OnMessagesReceived(List<PopupMessage> messages)
    {
        messages.ForEach(message => _messagesQueue.Enqueue(message));
    }

    void OnPopupActivated()
    {
        ProcessNextMessage();
    }

    void OnPopupClosed()
    {
        ProcessNextMessage();
    }

    void ProcessNextMessage()
    {
        if (_messagesQueue.TryDequeue(out var message))
        {
            _popupUIElement.SetPopupData(message);
        }
    }
}
