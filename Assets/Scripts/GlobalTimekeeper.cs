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
    private GameObject Player;
    private PlayerMovement playermovement;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        inst = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        playermovement = Player.GetComponent<PlayerMovement>();
    }

    public static void Tick()
    {
        inst.dotick.Invoke();
        inst.playticksound();
        print("tick");
        tickcount++;
    }
    public void playticksound() {
        StartCoroutine(delaySound());
    }
    IEnumerator delaySound() {
        
        yield return new WaitForSeconds(playermovement.timeout / 2);
        AudioSource.PlayClipAtPoint(inst.ticksound, inst.gameObject.transform.position, SettingsHolder.SFXvol);
    }
    public static void Tick(int n)
    {
        inst.dotick.Invoke();
        AudioSource.PlayClipAtPoint(inst.sounds[n], inst.gameObject.transform.position, SettingsHolder.SFXvol);
        tickcount++;
    }
}
