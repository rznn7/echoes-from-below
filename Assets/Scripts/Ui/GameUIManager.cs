using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameUIManager : MonoBehaviour
{
    public ValueToSprite oxygen;
    public ValueToSprite power;
    public ValueToSprite bullets;
    public ValueToSprite leak;
    public MovePanelInterpreter moveControls;
    public ParticleSystem[] bubbles;
    public static GameUIManager instance;
    public Button go;
    public TMP_Text scrapDisp;
    static string fmt = "00";

    private void Start()
    {
        go.onClick.AddListener(GlobalTimekeeper.Tick);
        toggleBubbles(false);

        
    }
    public static void toggleGoButton(bool a) {
        instance.go.interactable = a;
    }
    public static void initOxygen(float maxCapacity) {
        instance.oxygen.max = maxCapacity;
    }
    public static void initPower(float maxCapacity)
    {
        instance.power.max = maxCapacity;
    }
    public static void initLeak(float maxCapacity)
    {
        instance.leak.max = maxCapacity;
    }
    public static void updateScrap(int val) {
        instance.scrapDisp.text = ":" + val.ToString(fmt);
    }
    public static void updateOxygen(float val)
    {
        instance.oxygen.updateValue(val);
    }
    public static void updateBullets(float val)
    {
        instance.bullets.updateValue(val);
    }
    public static void updatePower(float val)
    {
        instance.power.updateValue(val);
    }
    public static void updateLeak(float val)
    {
        instance.leak.updateValue(val);
    }
    public static int getMove()
    {
        return instance.moveControls.movchoice;
    }
    public static void toggleBubbles(bool s) {
        ParticleSystem.EmissionModule em;
        em = instance.bubbles[0].emission;
        em.enabled = s;
        em = instance.bubbles[1].emission;
        em.enabled = s;
        em = instance.bubbles[2].emission;
        em.enabled = s;
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
