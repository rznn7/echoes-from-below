using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FixLeakEvent : MonoBehaviour, IHandleInteraction
{
    [SerializeField] private Button fixLeakButton;
    
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        
        fixLeakButton.onClick.AddListener(HandleInteraction);
    }

    private void OnDestroy()
    {
        fixLeakButton.onClick.RemoveListener(HandleInteraction);
    }
    
    private void FixLeak()
    {
        if (!(GameUIManager.instance.power.value >= playerStats.powerCostToRepair) ||
            playerStats.scrapCount < 1 || GameUIManager.instance.leak.value == 0) return;
            
        GameUIManager.updatePower(GameUIManager.instance.power.value - playerStats.powerCostToRepair); 
        playerStats.scrapCount--;
        GameUIManager.updateScrap(playerStats.scrapCount);
            
        float newLeakValue = GameUIManager.instance.leak.value - playerStats.leakReduction;
        GameUIManager.updateLeak(newLeakValue > 0 ? newLeakValue : 0);
    }

    public IEnumerator HandleEventInteraction()
    {
        EventInteraction interaction = FindObjectOfType<EventInteraction>();
        if (interaction == null) yield break;
        
        bool eventHandled = false;
        
        Action onAccept = () =>
        {
            FixLeak();
            eventHandled = true;
        };
        
        Action onDeny = () =>
        {
            eventHandled = true;
        };

        float currentLeakValue = GameUIManager.instance.leak.value;
        if (currentLeakValue == 0)
        {
            interaction.SendLeakDoesNotNeedFixingInteractionMessage(onAccept, onDeny);
            yield break;
        }

        bool enoughResources = GameUIManager.instance.power.value >= playerStats.powerCostToRepair && playerStats.scrapCount >= 1;
        
        float leakValue = currentLeakValue - playerStats.leakReduction;
        if (leakValue < 0)
            leakValue = 0;
        
        interaction.SendFixLeakInteractionMessage(enoughResources, Mathf.RoundToInt(leakValue), onAccept, onDeny);
        
        while (!eventHandled)
        {
            yield return null;
        }
    }

    private void HandleInteraction()
    {
        StartCoroutine(HandleEventInteraction());
    }
}
