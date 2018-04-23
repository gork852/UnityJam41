using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetActionLightning : targetAction {
    public int damage = 5;
    float timeToDie;
	// Use this for initialization
	void Start () {
        timeToDie = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeToDie!=0f && Time.time > timeToDie)
        {
            Destroy(this.gameObject);
        }
	}
    override public void actOnTarget(Card target)
    {
        target.applyDamage(damage);
        timeToDie = Time.time + 1;
        //Destroy(this.gameObject);
    }
}
