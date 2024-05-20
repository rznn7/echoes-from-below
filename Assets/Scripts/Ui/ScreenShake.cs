using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public Animator anim;
    private float t;
    private float duration;
    private bool shaking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shaking) {
            if (t >= duration) {
                anim.SetBool("shaking",false);
            }
            t += Time.deltaTime;
        }
    }

    public void shake(float tm) {
        duration = tm;
        t = 0;
        shaking = true;
        anim.SetBool("shaking", true);
    }
}
