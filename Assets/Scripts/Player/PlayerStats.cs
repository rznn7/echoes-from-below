using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float powerToSubtract = 10f;
    [SerializeField]
    private float oxygenToSubtract = 10f;

    public int scrapCount;
    public int powerCostToRepair = 2;
    public float leakReduction = 15f;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        playerMovement.OnPlayerMove += SubtractPowerAfterTurn;
        playerMovement.OnPlayerMove += SubtractOxygenAfterTurn;
        playerMovement.OnPlayerMove += ApplyLeakageAfterTurn;

        GameUIManager.InitPower(GameUIManager.instance.power.max);
        GameUIManager.InitOxygen(GameUIManager.instance.oxygen.max);
        GameUIManager.InitLeak(GameUIManager.instance.leak.max);
        GameUIManager.UpdateBullets(GameUIManager.instance.bullets.value);
    }

    private void OnDestroy()
    {
        playerMovement.OnPlayerMove -= SubtractPowerAfterTurn;
        playerMovement.OnPlayerMove -= SubtractOxygenAfterTurn;
        playerMovement.OnPlayerMove -= ApplyLeakageAfterTurn;
    }

    private void SubtractPowerAfterTurn()
    {
        GameUIManager.UpdatePower(GameUIManager.instance.power.value - powerToSubtract);
    }

    private void SubtractOxygenAfterTurn()
    {
        GameUIManager.UpdateOxygen(GameUIManager.instance.oxygen.value - oxygenToSubtract);
    }

    private void ApplyLeakageAfterTurn()
    {
        float currentLeak = GameUIManager.instance.leak.value;
        float additionalLeakage = 0f;

        if (currentLeak is > 20 and <= 50)
        {
            additionalLeakage = currentLeak * 0.03f;
        }
        else if (currentLeak is > 50 and <= 70)
        {
            additionalLeakage = currentLeak * 0.05f;
        }
        else if (currentLeak > 70)
        {
            additionalLeakage = currentLeak * 0.07f;
        }

        GameUIManager.UpdateLeak(currentLeak + additionalLeakage);
    }
}
