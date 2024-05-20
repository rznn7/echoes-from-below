using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerGameWinExample : MonoBehaviour
{
    public GameWinTrigger gameWinTrigger;

    void Start()
    {
        StartCoroutine(TriggerGameWinWithDelay(4f));
    }

    IEnumerator TriggerGameWinWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameWinTrigger.TriggerGameWin();
    }
}
