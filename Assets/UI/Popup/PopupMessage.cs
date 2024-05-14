public struct PopupMessage
{
    public string MessageText { get; }
    public string ButtonText { get; }

    public PopupMessage(string messageText, string buttonText)
    {
        MessageText = messageText;
        ButtonText = buttonText;
    }
}
