using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using UnityEngine;
using UnityEngine.Serialization;

public class TextualInteractionManager : MonoBehaviour
{
    [SerializeField]
    TextualInteractionStartUIElement textualInteractionStartUIElement;
    [SerializeField]
    TextualInteractionEndUIElement textualInteractionEndUIElement;

    Subject<bool> _interactionSubject;

    void Awake()
    {
        textualInteractionStartUIElement.AnswerInteraction += OnInteractionAnswered;
        textualInteractionEndUIElement.EndInteraction += OnInteractionEnded;

        textualInteractionStartUIElement.Hide();
        textualInteractionEndUIElement.Hide();
    }

    public IObservable<bool> StartInteraction(TextualInteraction interaction)
    {
        _interactionSubject = new Subject<bool>();


        textualInteractionStartUIElement.SetData(interaction);
        textualInteractionEndUIElement.SetData(interaction);

        textualInteractionStartUIElement.Show();

        return _interactionSubject.AsObservable();
    }

    void OnInteractionAnswered(bool answer)
    {
        textualInteractionStartUIElement.Hide();

        if (!answer)
        {
            _interactionSubject.OnNext(false);
            _interactionSubject.OnCompleted();
            return;
        }

        textualInteractionEndUIElement.Show();
    }

    void OnInteractionEnded()
    {
        textualInteractionEndUIElement.Hide();

        _interactionSubject.OnNext(true);
        _interactionSubject.OnCompleted();
    }
}
