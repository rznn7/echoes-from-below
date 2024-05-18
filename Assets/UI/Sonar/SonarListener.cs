using System.Collections;
using UnityEngine;

public class SonarListener : MonoBehaviour
{
    Animator _sonarElementAnimator;

    [SerializeField]
    float activationDuration = 5.0f;

    void Awake()
    {
        _sonarElementAnimator = GetComponent<Animator>();
    }

    public void OnActivate()
    {
        _sonarElementAnimator.SetTrigger(SonarAnimatorStringHash.Activate);
        StartCoroutine(DeactivateAfterDuration());
    }

    IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(activationDuration);
        _sonarElementAnimator.SetTrigger(SonarAnimatorStringHash.Disable);
    }
}

public static class SonarAnimatorStringHash
{
    public static readonly int Activate = Animator.StringToHash("Activate");
    public static readonly int Disable = Animator.StringToHash("Disable");
}
