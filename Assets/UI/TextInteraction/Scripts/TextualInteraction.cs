public class TextualInteraction
{
    public string InteractionStartText { get; }
    public string InteractionEndText { get; }

    public string InteractionAcceptButtonText { get; }
    public string InteractionDenyButtonText { get; }
    public string InteractionEndButtonText { get; }

    public TextualInteraction(
        string interactionStartText,
        string interactionEndText,
        string interactionAcceptButtonText,
        string interactionDenyButtonText,
        string interactionEndButtonText
    )
    {
        InteractionStartText = interactionStartText;
        InteractionEndText = interactionEndText;
        InteractionAcceptButtonText = interactionAcceptButtonText;
        InteractionDenyButtonText = interactionDenyButtonText;
        InteractionEndButtonText = interactionEndButtonText;
    }
}
