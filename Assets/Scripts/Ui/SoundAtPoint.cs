using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAtPoint : MonoBehaviour
{
    public void playClip(AudioClip ac) {
        AudioSource.PlayClipAtPoint(ac,Camera.main.transform.position,SettingsHolder.SFXvol);
    }
}
