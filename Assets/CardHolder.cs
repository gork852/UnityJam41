using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour {

    public List<Card> hand;
    public int handSize;
    public float handWidth = 4f;
    public float handHeight = .75f;
	// Use this for initialization
	void Start () {
        hand = new List<Card>();
        handSize = 0;
        foreach (Transform child in this.transform)
        {
            Card c = child.gameObject.GetComponent<Card>();
            if (c)
            {
                hand.Add(c);
                handSize++;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < hand.Count; i++)
        {
            hand[i].transform.position = this.transform.position + new Vector3(handWidth/handSize+i,handHeight/handSize+i,0);
        }
	}
}
