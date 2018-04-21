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
    public Transform slerpTo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (slerpTo != null)
        {
            Vector3 diff = this.transform.position - (slerpTo.transform.position + slerpTo.transform.up / 4);
            this.transform.position = this.transform.position - diff * Time.deltaTime;
            Quaternion rotation = Quaternion.AngleAxis(90, Vector3.right);
            this.transform.rotation = rotation;
        }


    }

    public void applyDamage(int damage)
    {
        curHealth -= damage;
    }



}
