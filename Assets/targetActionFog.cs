using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetActionFog : targetAction {
    private List<Card> tempHealthTargets;
    private List<int> oldHp;
    private float timeToDie;
	// Use this for initialization
	void Start () {
        tempHealthTargets = new List<Card>();
        oldHp = new List<int>();
	}
	
	// Update is called once per frame
	void Update () {
        if (timeToDie != 0f && Time.time > timeToDie)
        {
            while (tempHealthTargets.Count>0)
            {
                if (tempHealthTargets[0] != null)
                {
                    tempHealthTargets[0].curHealth = tempHealthTargets[0].curHealth > oldHp[0] ? oldHp[0] : tempHealthTargets[0].curHealth;
                }
                tempHealthTargets.RemoveAt(0);
                oldHp.RemoveAt(0);
            }
            Destroy(this.gameObject);
        }
        //Debug.Log(Time.time+":"+timeToDie);
	}
    public override void actOnTarget(Card target)
    {
        tempHealthTargets.Add(target);
        oldHp.Add(target.curHealth);
        target.curHealth += 3;
        timeToDie = Time.time + 6;
        Debug.Log("apply!");
    }
}
