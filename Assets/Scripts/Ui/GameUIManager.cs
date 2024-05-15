using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUIManager : MonoBehaviour
{
    public ValueToSprite oxygen;
    public ValueToSprite power;
    public MovePanelInterpreter moveControls;
    public static GameUIManager instance;
    public static void initOxygen(float maxCapacity) {
        instance.oxygen.max = maxCapacity;
    }
    public static void initPower(float maxCapacity)
    {
        instance.power.max = maxCapacity;
    }
    public static void updateOxygen(float val)
    {
        instance.oxygen.updateValue(val);
    }
    public static void updatePower(float val)
    {
        instance.power.updateValue(val);
    }
    public static void tickReset() {
        instance.moveControls.reset();
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
