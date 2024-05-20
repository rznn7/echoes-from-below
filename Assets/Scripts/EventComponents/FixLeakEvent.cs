using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FixLeakEvent : MonoBehaviour, IHandleInteraction
{
    [SerializeField] private Button fixLeakButton;
    [SerializeField] private GameObject leak;
    private PlayerStats playerStats;
    private leakData ld;
    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        ld = FindObjectOfType<leakData>();
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
            
        GameUIManager.UpdatePower(GameUIManager.instance.power.value - playerStats.powerCostToRepair); 
        playerStats.scrapCount--;
        GameUIManager.UpdateScrap(playerStats.scrapCount);
            
        float newLeakValue = GameUIManager.instance.leak.value - playerStats.leakReduction;

        if (ld.getActiveNumber() == 1) {
            if (newLeakValue > 20) {
                newLeakValue -= 10;
            }
        }
        GameUIManager.UpdateLeak(newLeakValue > 0 ? newLeakValue : 0);
        leak.SetActive(false);
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
        
        float modifiedLeakValue = currentLeakValue - playerStats.leakReduction;
        if (modifiedLeakValue < 0)
            modifiedLeakValue = 0;
        
        interaction.SendFixLeakInteractionMessage(enoughResources, currentLeakValue, Mathf.RoundToInt(modifiedLeakValue), onAccept, onDeny);
        
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
