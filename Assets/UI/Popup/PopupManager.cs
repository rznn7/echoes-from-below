using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public event Action MessageReceived;
    public event Action NoMoreMessageInQueue;

    PopupUIElement _popupUIElement;

    readonly Queue<Queue<PopupMessage>> _queuesOfMediumPriorityMessagesQueues = new();
    readonly Queue<Queue<PopupMessage>> _queuesOfHighPriorityMessagesQueues = new();
    Queue<PopupMessage> _currentQueue;

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
        UnsubscribeFromMessageSenders();
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

    void UnsubscribeFromMessageSenders()
    {
        FindObjectsOfType<MonoBehaviour>().OfType<IPopupMessageSender>().ToList()
            .ForEach(sender => sender.AddMessagesToQueue -= OnMessagesReceived);
    }

    void UnsubscribeFromPopupActivators()
    {
        FindObjectsOfType<MonoBehaviour>().OfType<IPopupActivator>().ToList()
            .ForEach(sender => sender.ActivatePopup -= OnPopupActivated);
    }

    void OnMessagesReceived(List<PopupMessage> messages, MessagePriority priority)
    {
        var messageQueue = new Queue<PopupMessage>(messages);

        if (priority == MessagePriority.High)
        {
            _queuesOfHighPriorityMessagesQueues.Enqueue(messageQueue);
        }
        else
        {
            _queuesOfMediumPriorityMessagesQueues.Enqueue(messageQueue);
        }
        MessageReceived?.Invoke();
    }

    public void OnPopupActivated()
    {
        if (_currentQueue == null)
        {
            ProcessNextQueue();
        }

        ProcessNextMessage();
    }

    void OnPopupClosed()
    {
        ProcessNextMessage();
        SendNoMoreMessageEventsIfQueuesEmpty();
    }

    void ProcessNextQueue()
    {
        if (_queuesOfHighPriorityMessagesQueues.Count > 0)
        {
            _currentQueue = _queuesOfHighPriorityMessagesQueues.Dequeue();
        }
        else if (_queuesOfMediumPriorityMessagesQueues.Count > 0)
        {
            _currentQueue = _queuesOfMediumPriorityMessagesQueues.Dequeue();
        }
    }

    void ProcessNextMessage()
    {
        if (_currentQueue == null || _currentQueue.Count == 0)
        {
            _currentQueue = null;
            return;
        }

        if (_currentQueue.TryDequeue(out var message))
        {
            _popupUIElement.SetPopupData(message);
        }

        if (_currentQueue.Count == 0)
        {
            _currentQueue = null;
        }
    }

    void SendNoMoreMessageEventsIfQueuesEmpty()
    {
        if (_queuesOfHighPriorityMessagesQueues.Count == 0 && _queuesOfMediumPriorityMessagesQueues.Count == 0)
        {
            NoMoreMessageInQueue?.Invoke();
        }
    }
}
