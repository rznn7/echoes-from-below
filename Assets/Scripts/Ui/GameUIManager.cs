using System;
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
    public leakData leak;
    public GameObject[] leaks;
    public MovePanelInterpreter moveControls;
    public ParticleSystem[] bubbles;
    public GameObject[] encounters;
    public GameObject[] enemies;
    public static GameUIManager instance;
    public Button go;
    public TMP_Text scrapDisp;
    static string fmt = "00";
    public ScreenShake ss;

    private void Start()
    {
        go.onClick.AddListener(GlobalTimekeeper.Tick);
        ToggleBubbles(false);
    }

    public static void ToggleGoButton(bool a)
    {
        instance.go.interactable = a;
    }

    public static void InitOxygen(float maxCapacity)
    {
        instance.oxygen.max = maxCapacity;
    }

    public static void InitPower(float maxCapacity)
    {
        instance.power.max = maxCapacity;
    }

    public static void InitLeak(float maxCapacity)
    {
        instance.leak.max = maxCapacity;
    }

    public static void UpdateLeak(float val)
    {
        if (val > instance.leak.value)
        {
            instance.ss.shake(0.35f);
        }
        instance.leak.updateValue(val);
    }

    public static void UpdateScrap(int val)
    {
        instance.scrapDisp.text = ":" + val.ToString(fmt);
    }

    public static void UpdateOxygen(float val)
    {
        instance.oxygen.updateValue(val);
    }

    public static void UpdateBullets(float val)
    {
        instance.bullets.updateValue(val);
    }

    public static void UpdatePower(float val)
    {
        instance.power.updateValue(val);
    }

    public static int GetMove()
    {
        return instance.moveControls.movchoice;
    }

    public static void ToggleBubbles(bool s)
    {
        ParticleSystem.EmissionModule em;
        em = instance.bubbles[0].emission;
        em.enabled = s;
        em = instance.bubbles[1].emission;
        em.enabled = s;
        em = instance.bubbles[2].emission;
        em.enabled = s;
    }

    public static void TickReset()
    {
        instance.moveControls.reset();
        EventDisp(-1);
        EnemyDisp(-1);
    }

    /// <summary>
    /// -1 : clear
    /// 0 : sea mine
    /// 1 : shipwreck
    /// 2 : shoal
    /// 3 : scrap
    /// </summary>
    /// <returns></returns>
    public static void EventDisp(int n)
    {
        for (int i = 0; i < instance.encounters.Length; i++)
        {
            instance.encounters[i].SetActive(i == n);
        }
    }

    /// <summary>
    /// -1 : clear
    /// 0 : shark
    /// 1 : anglerfish
    /// </summary>
    /// <returns></returns>
    public static void EnemyDisp(int n)
    {
        for (int i = 0; i < instance.enemies.Length; i++)
        {
            instance.enemies[i].SetActive(i == n);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
