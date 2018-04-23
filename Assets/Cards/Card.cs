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

    public TimedKeyPress timedPress;

    public bool hasMoved = false;
    public Transform slerpTo;

    public enum cardState{inDeck,inhand,onboard,ingrave,inexile}
    public cardState state;
    public enum cardType {creature,  targetCreature, targetRow, targetBoard, targetCol}
    public cardType type;

    private GameObject effectHolder;

    // Use this for initialization
    void Start () {
        //state = cardState.inexile;
        effectHolder = new GameObject();
        effectHolder.transform.parent = this.transform;
        effectHolder.transform.localPosition = new Vector3(0, 0, 0);
        effectHolder.transform.localScale = new Vector3(1, 1, 1);       
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
        Vector3 scale = actor.transform.localScale;
        
        actor.transform.parent = this.effectHolder.transform;
        actor.transform.localScale = scale;
        actor.transform.position = this.transform.position + new Vector3(0,2,0);
        actor.state = Card.cardState.onboard;
        targetAction acton = actor.GetComponent<targetAction>();
        acton.actOnTarget(this);
    }
    public void applyDamage(int damage)
    {
        curHealth -= damage;
    }



}
