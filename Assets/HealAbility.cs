using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability {
    Card thisCard;
	// Use this for initialization
	void Start () {
        thisCard = this.GetComponent<Card>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void action(Board b)
    {
        BoardPosition target = b.getBoardPosition(thisCard.row + thisCard.dir, thisCard.col);
        //if (target == null)
        if(target!=null&&target.unitCard!=null)
            target.unitCard.curHealth += 3;
    }
}
