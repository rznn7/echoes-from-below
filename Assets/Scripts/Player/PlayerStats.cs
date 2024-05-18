using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float powerToSubtract = 10f;
    [SerializeField] private float oxygenToSubtract = 10f;

    public int scrapCount;
    
    private PlayerMovement _playerMovement;
    
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        
        _playerMovement.OnPlayerMove += SubtractPowerAfterTurn;
        _playerMovement.OnPlayerMove += SubtractOxygenAfterTurn;
        
        GameUIManager.initPower(GameUIManager.instance.power.max);
        GameUIManager.initOxygen(GameUIManager.instance.oxygen.max);
        GameUIManager.initLeak(GameUIManager.instance.leak.min);
    }

    private void OnDestroy()
    {
        _playerMovement.OnPlayerMove -= SubtractPowerAfterTurn;
        _playerMovement.OnPlayerMove -= SubtractOxygenAfterTurn;
    }

    private void SubtractPowerAfterTurn()
    {
        GameUIManager.updatePower(GameUIManager.instance.power.value - powerToSubtract);
    }
    
    private void SubtractOxygenAfterTurn()
    {
        GameUIManager.updateOxygen(GameUIManager.instance.oxygen.value - oxygenToSubtract);
    }
}
