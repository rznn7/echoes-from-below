using System.Collections;
using UnityEngine;

public class TriggerGameOverExample : MonoBehaviour
{
    public GameOverTrigger gameOverTrigger;

    void Start()
    {
        StartCoroutine(TriggerGameOverWithDelay(4f));
    }

    IEnumerator TriggerGameOverWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameOverTrigger.TriggerGameOver(GameOverType.Leakage);
    }
}
