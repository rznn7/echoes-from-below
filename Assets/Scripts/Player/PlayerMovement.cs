using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event Action OnPlayerMove;
    
    private Vector3 startpos = new Vector3(0, 0, 0);
    public Vector3 endpos = new Vector3(0, 0, 0);
    private float startrot = 0;
    private float endrot = 0;
    public AnimationCurve rotcurve;
    public AnimationCurve movcurve;
    private float t = 0;
    public float timeout;
    public bool inTimeout;
    private float ylev;
    private Vector3[] directions = {
        new Vector3(0, 1, 0),
        new Vector3(0, -1, 0),
        new Vector3(1,0,0),
        new Vector3(-1,0,0),
        new Vector3(0,0,-90),
        new Vector3(0,0,90) };
    // Start is called before the first frame update
    void Start()
    {
        ylev = transform.position.y;
        inTimeout = false;
        GlobalTimekeeper.inst.dotick.AddListener(onTick);
        GameUIManager.toggleBubbles(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inTimeout)
        {
            if (t >= timeout)
            {
                t = 0;
                GameUIManager.toggleGoButton(true);
                GameUIManager.toggleBubbles(false);
                inTimeout = false;
                this.transform.position = endpos;
                this.transform.eulerAngles = new Vector3(0, endrot, 0);
                OnPlayerMove?.Invoke();
                StartCoroutine(DetectEventAfterMove());
            }
            else
            {

                this.transform.eulerAngles = new Vector3(0,Mathf.Lerp(startrot, endrot, rotcurve.Evaluate(t / timeout)),0);
                this.transform.position = Vector3.Lerp(startpos, endpos, movcurve.Evaluate(t / timeout));
                t += Time.deltaTime;
            }
        }
    }

    void onTick()
    {
        StartCoroutine(DetectEventRoutine());
    }

    private IEnumerator DetectEventRoutine()
    {
        GameUIManager.toggleGoButton(false);
        int movC = GameUIManager.getMove();
        if (movC != -1)
        {
            GameUIManager.toggleBubbles(true);
            Vector3 dir = directions[movC];
            startpos = this.transform.position;
            bool hits = Physics.Linecast(startpos, startpos + new Vector3(dir.x, ylev, dir.y), (1 << 7), QueryTriggerInteraction.Collide);
            endpos = startpos + (new Vector3(dir.x, ylev, dir.y) * ((hits) ? 0 : 1));
            startrot = this.transform.eulerAngles.y;
            endrot = startrot + dir.z;
        }
        else
        {
            startrot = this.transform.eulerAngles.y;
            endrot = startrot;
            startpos = this.transform.position;
            endpos = startpos;
        }

        inTimeout = true;
        t = 0;
        yield return null;
    }
    
    private IEnumerator DetectEventAfterMove()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 1f))
        {
            if (hit.collider.CompareTag("Event"))
            {
                var interaction = hit.collider.GetComponent<IHandleInteraction>();
                if (interaction != null)
                {
                    yield return interaction.HandleEventInteraction();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
