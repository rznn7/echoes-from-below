using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject Player;
    private PlayerMovement playermovement;
    private PlayerStats playerStats;
    public float clipping;
    private float t = 0;
    private float timeout;
    private float tiredtimer = 0;
    public float stamina;
    private Vector3 startpos;
    private Vector3 endpos;
    private bool moving = false;
    public AnimationCurve movcurve;
    private bool insight;
    private Vector3 lastknown;

    public Vector2 damageRange;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playermovement = Player.GetComponent<PlayerMovement>();
        GlobalTimekeeper.inst.dotick.AddListener(tick);
        startpos = this.transform.position;
        insight = false;
        timeout = playermovement.timeout;
    }

    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            t += Time.deltaTime;
            Vector3 newpos = new Vector3();
            newpos = Vector3.Lerp(startpos, endpos, movcurve.Evaluate(t / timeout));
            this.transform.position = newpos;
        }

    }
    void tick()
    {

        StartCoroutine(pathfinder());

    }

    IEnumerator pathfinder() {
        yield return new WaitForEndOfFrame();
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x), this.transform.position.y, Mathf.Round(this.transform.position.z));
        endpos = this.transform.position;
        if (tiredtimer < stamina)
        {
            if (Mathf.Abs(Vector3.Distance(playermovement.endpos, this.transform.position)) < clipping)
            {

                bool hitwall = Physics.Linecast(this.transform.position, Player.transform.position, (1 << 7));
                if (!hitwall)
                {
                    insight = true;
                    if (Mathf.Abs(Vector3.Distance(playermovement.endpos, this.transform.position)) > 1)
                    {
                        startpos = this.transform.position;

                        float[] scores = { Mathf.Abs(Vector3.Distance(this.transform.position + this.transform.forward, playermovement.endpos)), Mathf.Abs(Vector3.Distance(this.transform.position + this.transform.right, playermovement.endpos)), Mathf.Abs(Vector3.Distance(this.transform.position - this.transform.forward, playermovement.endpos)), Mathf.Abs(Vector3.Distance(this.transform.position - this.transform.right, playermovement.endpos)) };
                        int[] indexes = { 0, 1, 2, 3 };

                        float tmp;
                        int ti;
                        for (int j = 0; j <= scores.Length - 2; j++)
                        {
                            for (int i = 0; i <= scores.Length - 2; i++)
                            {
                                if (scores[i] > scores[i + 1])
                                {
                                    tmp = scores[i + 1];
                                    ti = indexes[i + 1];
                                    scores[i + 1] = scores[i];
                                    scores[i] = tmp;
                                    indexes[i + 1] = indexes[i];
                                    indexes[i] = ti;
                                }
                            }
                        }

                        List<int> paths = new List<int>();

                        foreach (int i in indexes)
                        {
                            if (feasiblepath(i + 1))
                            {
                                paths.Add(i + 1);
                            }
                        }

                        if (paths.Count != 0)
                        {
                            Vector3 dir;
                            if (paths[0] == 1)
                            {
                                dir = this.transform.forward;
                            }
                            else if (paths[0] == 2)
                            {
                                dir = this.transform.right;

                            }
                            else if (paths[0] == 3)
                            {
                                dir = -this.transform.forward;

                            }
                            else
                            {
                                dir = -this.transform.right;
                            }


                            t = 0;
                            endpos = startpos + dir;
                            moving = true;
                            insight = true;
                            lastknown = playermovement.endpos;
                        }
                    }
                    else
                    {
                        //what to do when enemy touches player
                        float leakIncrease = Random.Range(damageRange.x,damageRange.y);
                        GameUIManager.updateLeak(GameUIManager.instance.leak.value + leakIncrease);
                    }
                }
                else
                {
                    if (insight)
                    {
                        startpos = this.transform.position;

                        float[] scores = { Mathf.Abs(Vector3.Distance(this.transform.position + this.transform.forward, lastknown)), Mathf.Abs(Vector3.Distance(this.transform.position + this.transform.right, lastknown)), Mathf.Abs(Vector3.Distance(this.transform.position - this.transform.forward, lastknown)), Mathf.Abs(Vector3.Distance(this.transform.position - this.transform.right, lastknown)) };
                        int[] indexes = { 0, 1, 2, 3 };

                        float tmp;
                        int ti;
                        for (int j = 0; j <= scores.Length - 2; j++)
                        {
                            for (int i = 0; i <= scores.Length - 2; i++)
                            {
                                if (scores[i] > scores[i + 1])
                                {
                                    tmp = scores[i + 1];
                                    ti = indexes[i + 1];
                                    scores[i + 1] = scores[i];
                                    scores[i] = tmp;
                                    indexes[i + 1] = indexes[i];
                                    indexes[i] = ti;
                                }
                            }
                        }

                        List<int> paths = new List<int>();

                        foreach (int i in indexes)
                        {
                            if (feasiblepath(i + 1))
                            {
                                paths.Add(i + 1);
                            }
                        }

                        if (paths.Count != 0)
                        {
                            Vector3 dir;
                            if (paths[0] == 1)
                            {
                                dir = this.transform.forward;
                            }
                            else if (paths[0] == 2)
                            {
                                dir = this.transform.right;

                            }
                            else if (paths[0] == 3)
                            {
                                dir = -this.transform.forward;

                            }
                            else
                            {
                                dir = -this.transform.right;
                            }


                            t = 0;
                            endpos = startpos + dir;
                            moving = true;
                            insight = false;

                        }
                    }



                }
            }
            tiredtimer++;
        }
        else
        {
            tiredtimer = 0;
        }
    }
    public bool feasiblepath(int inp)
    {

        RaycastHit hit = new RaycastHit();
        Vector3 dir = new Vector3();
        if (inp == 1)
        {
            dir = this.transform.forward;
        }
        else if (inp == 2)
        {
            dir = this.transform.right;

        }
        else if (inp == 3)
        {
            dir = -this.transform.forward;

        }
        else
        {
            dir = -this.transform.right;

        }

        bool hits = Physics.Linecast(this.transform.position, this.transform.position + dir, out hit, (1 << 7) | (1 << 6) | (1 << 8)|(1<<10),QueryTriggerInteraction.Collide);
        if (hits)
        {

            return false;

        }
        if (this.transform.position + dir == playermovement.endpos) {
            return false;
        }
        return true;
    }
}
