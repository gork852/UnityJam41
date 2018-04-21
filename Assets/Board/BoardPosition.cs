using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPosition : MonoBehaviour {

    public Card unitCard = null;
    public int row;
    public int col;

    List<string> modifiers = new List<string>();

    public BoardPosition(int row, int col, string modifier)
    {
        this.row = row;
        this.col = col;
        modifiers.Add(modifier);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {









	}

    public bool isPlayableHere(Card card)
    {
        if (unitCard == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
