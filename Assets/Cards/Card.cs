using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    string name;

    public int baseAttack;
    int maxHealth;
    public int curHealth;

    public int beatSpeed;
    public int beatsRemaining; //Beats remaining until next move

    public int row;
    public int col;
    public int unitRange;
    public int dir;

    public bool hasMoved = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void applyDamage(int damage)
    {
        curHealth -= damage;
    }

}
