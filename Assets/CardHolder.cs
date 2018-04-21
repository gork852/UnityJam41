using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour {

    public List<Card> hand;
    public int handSize;
    public float totalHandAngle = 30f;
    public float handWidth = 4;
    public float handHeight = 2;

    private List<GameObject> idealLocations;
    private GameObject phantomHand;
    // Use this for initialization
    void Start () {
        hand = new List<Card>();
        phantomHand = new GameObject();
        phantomHand.name = "phantomHand";
        phantomHand.transform.position = this.transform.position;
        phantomHand.transform.rotation = this.transform.rotation;
        idealLocations = new List<GameObject>();
        handSize = 0;
        foreach (Transform child in this.transform)
        {
            Debug.Log("got one");
            Card c = child.gameObject.GetComponent<Card>();
            if (c)
            {
                idealLocations.Add(new GameObject());
                idealLocations[handSize].name = "Card Position " + handSize;
                idealLocations[handSize].transform.parent = phantomHand.transform;
                hand.Add(c);
                handSize++;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {

        //Quaternion temp = this.transform.rotation;
        //this.transform.rotation = new Quaternion();
		for(int i = 0; i < hand.Count; i++)
        {
            float anglePercent = i / ((float)handSize - 1);

            idealLocations[i].transform.rotation = new Quaternion();
            idealLocations[i].transform.position = this.transform.position + new Vector3(-handWidth + anglePercent * 2 * handWidth, handHeight * Mathf.Pow(Mathf.Sin(anglePercent * Mathf.PI), 1), anglePercent * .1f);
            idealLocations[i].transform.Rotate(new Vector3(0, 0, 1), totalHandAngle-anglePercent*2*totalHandAngle);
            

            /*hand[i].transform.rotation = new Quaternion();
            hand[i].transform.position = this.transform.position + new Vector3(-handWidth + anglePercent * 2 * handWidth, handHeight * Mathf.Pow(Mathf.Sin(anglePercent * Mathf.PI), 1), anglePercent * .1f);
            hand[i].transform.Rotate(new Vector3(0, 0, 1), totalHandAngle - anglePercent * 2 * totalHandAngle);*/
            Vector3 diff = hand[i].transform.position - idealLocations[i].transform.position;
            hand[i].transform.position = hand[i].transform.position - diff * Time.deltaTime;
            hand[i].transform.rotation = idealLocations[i].transform.rotation;
        }
        //this.transform.rotation = temp;
	}
}
