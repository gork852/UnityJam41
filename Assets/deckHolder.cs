using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckHolder : MonoBehaviour {

    public CardHolder hand;

    public List<GameObject> cardsPrefabs;
    public GameObject nullPrefab;
    public GameObject hiddenFacing;

    private List<GameObject> fakeDeck;
	// Use this for initialization
	void Start () {
        fakeDeck = new List<GameObject>();
		for(int i = 0; i < 30; i++)
        {
            GameObject card = Instantiate(nullPrefab);
            fakeDeck.Add(card);
            card.transform.parent = this.transform;
            card.transform.rotation = hiddenFacing.transform.rotation;
            card.transform.position = this.transform.position+new Vector3(0, i * .05f,0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (hand.hand.Count < 6)
        {
            GameObject addC = Instantiate(cardsPrefabs[Random.Range(0, cardsPrefabs.Count - 1)]);
            addC.transform.position = fakeDeck[fakeDeck.Count - 1].transform.position;
            hand.addCardToHand(addC);
        }
	}
}
