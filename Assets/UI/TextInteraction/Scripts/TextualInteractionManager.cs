using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using UnityEngine;

public class TextualInteractionManager : MonoBehaviour
{
    TextualInteractionStartUIElement _textualInteractionStartUIElement;
    TextualInteractionEndUIElement _textualInteractionEndUIElement;

    Subject<bool> _interactionSubject;

    void Start()
    {
        _textualInteractionStartUIElement = GetComponentInChildren<TextualInteractionStartUIElement>();
        _textualInteractionEndUIElement = GetComponentInChildren<TextualInteractionEndUIElement>();

        _textualInteractionStartUIElement.AnswerInteraction += OnInteractionAnswered;
        _textualInteractionEndUIElement.EndInteraction += OnInteractionEnded;

        _textualInteractionStartUIElement.Hide();
        _textualInteractionEndUIElement.Hide();
    }

    public IObservable<bool> StartInteraction(TextualInteraction interaction)
    {
        _interactionSubject = new Subject<bool>();

        _textualInteractionStartUIElement.SetData(interaction);
        _textualInteractionStartUIElement.Show();

        return _interactionSubject.AsObservable();
    }

    void OnInteractionAnswered(bool answer)
    {
        _textualInteractionStartUIElement.Hide();

        if (answer)
        {
            _textualInteractionEndUIElement.SetData(new TextualInteraction("",
                "Interaction Accepted",
                "",
                "",
                "Close"));
            _textualInteractionEndUIElement.Show();
        }
        else
        {
            _interactionSubject.OnNext(false);
            _interactionSubject.OnCompleted();
        }
    }

    void OnInteractionEnded()
    {
        _textualInteractionEndUIElement.Hide();
        _interactionSubject.OnNext(true);
        _interactionSubject.OnCompleted();
    }
}
