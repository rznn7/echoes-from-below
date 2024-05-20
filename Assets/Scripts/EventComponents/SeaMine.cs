using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeaMine : MonoBehaviour, IHandleInteraction
{
    private static bool firstMineEncounter = true;
    
    private bool interactable = true;

    private void Update()
    {
        if (PlayerOnMine())
        {
            Explode();
        }
    }

    public bool Interactable => interactable;
    
    public IEnumerator HandleEventInteraction()
    {
        if (!interactable) yield break;
        
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        if (interaction == null) yield break;
        
        GameUIManager.eventDisp(0);

        bool eventHandled = false;
        
        Action onAccept = () => 
        {
            eventHandled = true;
        };

        Action onDeny = () =>
        {
            eventHandled = true;
        };

        bool explode;

        if (firstMineEncounter)
        {
            explode = false;
            firstMineEncounter = false;
        }
        else
        {
            explode = Random.value < 0.5f;
        }

        if (explode)
        {
            interaction.SendSeaMineInteractionMessage(true, 0,
                () => {
                    onAccept.Invoke();
                    DealDamage();
                    gameObject.SetActive(false);
                }, onDeny);
        }
        else
        {
            int scrapToCollect = Random.Range(1, 4);
            interaction.SendSeaMineInteractionMessage(false, scrapToCollect,
                () => {
                    onAccept.Invoke();
                    PlayerStats playerStats = FindObjectOfType<PlayerStats>();
                    if (playerStats != null)
                    {
                        playerStats.scrapCount += scrapToCollect;
                        GameUIManager.updateScrap(playerStats.scrapCount);
                        gameObject.SetActive(false);
                    }
                }, onDeny);
        }
        
        while (!eventHandled)
        {
            yield return null;
        }

        GameUIManager.eventDisp(-1);
        interactable = false;
    }
    
    private void DealDamage()
    {
        float leakIncrease = Random.Range(30, 60);
        GameUIManager.updateLeak(GameUIManager.instance.leak.value + leakIncrease);
    }
    
    public void Explode()
    {
        DealDamage();
        gameObject.SetActive(false);
    }
    
    private bool PlayerOnMine()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.rotation);
        
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player") && !interactable)
            {
                PlayerMovement player = col.GetComponent<PlayerMovement>();
                if (player.transform.position == transform.position)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
    }
}
