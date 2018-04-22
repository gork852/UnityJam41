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

    public enum cardState{inDeck,inhand,onboard,ingrave,inexile}
    public cardState state;
    public enum cardType {creature,  targetCreature, targetRow, targetBoard, targetCol}
    public cardType type;

	// Use this for initialization
	void Start () {
        //state = cardState.inexile;
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
    public bool isValidTarget(Card actor)
    {
        if (this.state == Card.cardState.onboard && this.type==Card.cardType.creature && actor.type == Card.cardType.targetCreature)
        {
            return true;
        }
        return false;
    }
    public void playCardToThis(Card actor)
    {
        actor.transform.parent = this.transform;
        actor.transform.position = this.transform.position + new Vector3(0,2,0);
        actor.state = Card.cardState.onboard;
        targetAction acton = actor.GetComponent<targetAction>();
        Debug.Log(curHealth);
        acton.actOnTarget(this);
        Debug.Log("WINNER");
        Debug.Log(curHealth);
    }
    public void applyDamage(int damage)
    {
        curHealth -= damage;
    }



}
