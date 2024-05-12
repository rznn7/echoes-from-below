using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GlobalTimekeeper : MonoBehaviour
{

    public static GlobalTimekeeper inst;
    public static int tickcount;
    public AudioClip ticksound;
    public AudioClip[] sounds;
    public UnityEvent dotick;
   
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        inst = this;
    }

    public static void Tick()
    {
        inst.dotick.Invoke();
        //AudioSource.PlayClipAtPoint(inst.ticksound, inst.gameObject.transform.position, SettingsHolder.SFXvol);
        print("tick");
        tickcount++;
    }
    public static void Tick(int n)
    {
        inst.dotick.Invoke();
        AudioSource.PlayClipAtPoint(inst.sounds[n], inst.gameObject.transform.position, SettingsHolder.SFXvol);
        tickcount++;
    }
}
