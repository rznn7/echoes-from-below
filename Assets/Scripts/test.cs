using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class test : MonoBehaviour
{
    bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tst(InputAction.CallbackContext ctx) {
        if (ctx.ReadValueAsButton() && !state)
        {
            GlobalTimekeeper.Tick();
            state = !state;
        }
        else {
            state = false;
        }
    }
}
