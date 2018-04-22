using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetActionLightning : targetAction {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    override public void actOnTarget(Card target)
    {
        target.applyDamage(5);
        Destroy(this.gameObject);
    }
}
