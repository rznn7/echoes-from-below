using System;
using System.Collections.Generic;

public interface IPopupMessageSender
{
    public event Action<List<PopupMessage>, MessagePriority> AddMessagesToQueue;
}
