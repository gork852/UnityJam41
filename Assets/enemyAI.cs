using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour {

    public CardHolder enemyHand;
    public Board board;
    public GameObject enemyIndprfb;
    private float lastPlayed;
    public float playInterval = 12;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > lastPlayed + playInterval)
        {
            lastPlayed = Time.time;
            int handIndex = Random.Range(0, enemyHand.hand.Count - 1);
            Debug.Log(handIndex);
            Card wantPlay = enemyHand.hand[handIndex];
            if(wantPlay.type == Card.cardType.creature)
            {
                int col = Random.Range(0, 5);
                if (board.isPlayableAtPosition(wantPlay, 0, col))
                {

                    
                    board.addCardToBoard(enemyHand.removeCard(wantPlay), 0, col, enemyHand.dir);
                    GameObject ind = Instantiate(enemyIndprfb);
                    ind.transform.position = wantPlay.transform.position - wantPlay.transform.forward * .05f;
                    ind.transform.rotation = wantPlay.transform.rotation;
                    ind.transform.parent = wantPlay.transform;
                }
            }
            if(wantPlay.type == Card.cardType.targetCreature)
            {
                List<Card> freindly = new List<Card>();
                List<Card> hostile = new List<Card>();
                foreach (BoardPosition pos in board.boardPositions)
                {
                    if(pos.unitCard!=null && pos.unitCard.dir == enemyHand.dir)
                    {
                        freindly.Add(pos.unitCard);
                    }
                    else if (pos.unitCard!=null && pos.unitCard.dir != enemyHand.dir)
                    {
                        hostile.Add(pos.unitCard);
                    }
                }
                int dmg = wantPlay.GetComponent<targetActionLightning>().damage;
                if (dmg > 0 && hostile.Count>0)
                {
                    hostile[Random.Range(0, hostile.Count - 1)].playCardToThis(enemyHand.removeCard(wantPlay));
                }
                else if(freindly.Count>0 && dmg<=0)
                {
                    freindly[Random.Range(0, freindly.Count - 1)].playCardToThis(enemyHand.removeCard(wantPlay));
                }
            }
            if(wantPlay.type == Card.cardType.targetBoard)
            {
                foreach (BoardPosition pos in enemyHand.board.boardPositions)
                {
                    if (pos.unitCard != null && pos.unitCard.type == Card.cardType.creature)
                    {
                        Debug.Log("attempt");
                        wantPlay.GetComponent<targetAction>().actOnTarget(pos.unitCard);
                    }
                }
                enemyHand.removeCard(wantPlay);
            }
        }
	}
}
