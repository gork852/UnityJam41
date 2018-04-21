using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour {

    public List<Card> hand;
    private List<GameObject> idealLocations;
    public int handSize;
    public float totalHandAngle = 30f;
    public float handWidth = 4;
    public float handHeight = 2;
	// Use this for initialization
	void Start () {
        hand = new List<Card>();
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
                idealLocations[handSize].transform.parent = this.transform;
                hand.Add(c);
                handSize++;
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < hand.Count; i++)
        {
            float anglePercent = i / ((float)handSize - 1);
            idealLocations[i].transform.rotation = new Quaternion();
            idealLocations[i].transform.position = this.transform.position + new Vector3(0, handHeight, 0);
            //idealLocations[i].transform.Rotate(new Vector3(), (i /(float) handSize - 1)*3);

            hand[i].transform.rotation = new Quaternion();
            hand[i].transform.position = this.transform.position + new Vector3(-handWidth+anglePercent*2*handWidth, handHeight*Mathf.Pow(Mathf.Sin(anglePercent*Mathf.PI),1), anglePercent*.1f);
            //hand[i].transform.rotation = new Quaternion(0, 0, 1, i/((float)handSize-1));

            hand[i].transform.Rotate(new Vector3(0, 0, 1), totalHandAngle-anglePercent*2*totalHandAngle);
        }
	}
}
