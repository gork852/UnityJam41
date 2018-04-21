using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour {

    public List<Card> hand;
    public Board board;
    public float totalHandAngle = 30f;
    public float handWidth = 4;
    public float handHeight = 2;
    public int dir = 0;
    public int startRow;

    public GameObject testprefab;

    private List<GameObject> idealLocations;
    private GameObject phantomHand;
    // Use this for initialization
    void Start () {
        int handSize;
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
                hand.Add(c);
                idealLocations.Add(new GameObject());
                idealLocations[hand.Count - 1].name = "Card Position " + (hand.Count - 1);
                idealLocations[hand.Count - 1].transform.parent = phantomHand.transform;
                
                handSize++;
            }
        }
	}
    private float rep = 3;
	// Update is called once per frame
	void Update () {
        phantomHand.transform.position = this.transform.position;
        phantomHand.transform.rotation = this.transform.rotation;
        //Quaternion temp = this.transform.rotation;
        //this.transform.rotation = new Quaternion();
        if (Time.time > rep && false)
        {
            Card c = removeRandomCard();
            if (c != null)
                board.addCardToBoard(c, startRow, (int)Random.Range(0, 6), dir);
            else
                Debug.Log("Null Card");
            //if(c) Destroy(c.gameObject);
            rep = Time.time + 3;
        }
        if (testprefab)
        {
            addDemoCardToHand();
            testprefab = null;
        }

		for(int i = 0; i < hand.Count; i++)
        {
            if (hand.Count == 1)
            {
                idealLocations[i].transform.rotation = new Quaternion();
                idealLocations[i].transform.localPosition = new Vector3(0, handHeight, 0);
                idealLocations[i].transform.Rotate(new Vector3(0, 0, 1), 0f);
            }
            else {
                float anglePercent;
                if (hand.Count > 6)
                {
                    anglePercent = i / ((float)hand.Count - 1);
                }
                else
                {
                    float phantomUsed = 6 - hand.Count;
                    float usedLeft = phantomUsed / 2;
                    anglePercent = (i+usedLeft) / (6-1); 
                }
                
                //Debug.Log(anglePercent);
                //.Log(i + (6 - handSize) / 2);
                idealLocations[i].transform.rotation = new Quaternion();
                //Debug.Log(-handWidth + anglePercent * 2 * handWidth);
                //Debug.Log(handHeight * Mathf.Pow(Mathf.Sin(anglePercent * Mathf.PI), 1));
                //Debug.Log(anglePercent * .1f);
                idealLocations[i].transform.localPosition = new Vector3(-handWidth + anglePercent * 2 * handWidth, handHeight * Mathf.Pow(Mathf.Sin(anglePercent * Mathf.PI), 1), anglePercent * .1f);
                idealLocations[i].transform.Rotate(new Vector3(0, 0, 1), totalHandAngle - anglePercent * 2 * totalHandAngle);
            }

            /*hand[i].transform.rotation = new Quaternion();
            hand[i].transform.position = this.transform.position + new Vector3(-handWidth + anglePercent * 2 * handWidth, handHeight * Mathf.Pow(Mathf.Sin(anglePercent * Mathf.PI), 1), anglePercent * .1f);
            hand[i].transform.Rotate(new Vector3(0, 0, 1), totalHandAngle - anglePercent * 2 * totalHandAngle);*/
            Vector3 diff = hand[i].transform.position - idealLocations[i].transform.position;
            hand[i].transform.position = hand[i].transform.position - diff * Time.deltaTime;
            hand[i].transform.rotation = idealLocations[i].transform.rotation;
            
        }
        //this.transform.rotation = temp;
	}
    void addCardToHand(GameObject card)
    {
        card.transform.parent = this.transform;
        Debug.Log("got one");
        Card c = card.gameObject.GetComponent<Card>();
        if (c)
        {
            hand.Add(c);
            idealLocations.Add(new GameObject());
            idealLocations[hand.Count - 1].name = "Card Position " + (hand.Count - 1);
            idealLocations[hand.Count-1].transform.parent = phantomHand.transform;
            
        }
    }
    void addDemoCardToHand()
    {
        this.addCardToHand(Instantiate(testprefab));
    }
    Card removeCard(int index)
    {
        Card tmp = hand[index];
        hand.RemoveAt(index);
        idealLocations.RemoveAt(index);
        return tmp;
    }
    Card removeCard(GameObject index)
    {
        return removeCard(index.GetComponent<Card>());
    }
    Card removeCard(Card index)
    {
        return removeCard(hand.IndexOf(index));
    }
    Card removeRandomCard()
    {
        if (hand.Count>0)
            return removeCard(Random.Range(0, hand.Count));
        else
            return null;
    }
}
