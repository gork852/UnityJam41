using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    float beatGap = 1.0f;
    float lastTime;

    List<BoardPosition> boardPositions = new List<BoardPosition>();
    Dictionary<int, Dictionary<int, BoardPosition>> indexedBoardPosition = new Dictionary<int, Dictionary<int, BoardPosition>>();

    // Use this for initialization
    void Start() {

        lastTime = Time.time;
        BoardPosition[] tempList = this.GetComponentsInChildren<BoardPosition>();

        Debug.Log("Board Postions found: " + boardPositions.Count);

        foreach (BoardPosition position in tempList)
        {
            Dictionary<int, BoardPosition> outValue;

            if (indexedBoardPosition.TryGetValue(position.row, out outValue))
            {
                if (!outValue.ContainsKey(position.col))
                {
                    outValue.Add(position.col, position);
                }
            }
            else
            {
                outValue = new Dictionary<int, BoardPosition>();
                outValue.Add(position.col, position);
                indexedBoardPosition.Add(position.row, outValue);
            }

            boardPositions.Add(position);
        }

    }

    // Update is called once per frame
    void Update() {
        

        if (isBeat())
        {
            //Debug.Log("Next Beat Start");
            attackPhase();
            damagePhase();
            movePhase();
        }
        updateBeats();

    }

    public void addCardToBoard(Card card, int row, int col, int dir)
    {
        BoardPosition pos = getBoardPosition(row, col);
        if(pos!= null && pos.unitCard == null)
        {
            pos.unitCard = card;
            card.slerpTo = pos.transform;
            card.dir = dir;
            card.row = row;
            card.col = col;
        }
        else
        {
            Destroy(card.gameObject);
        }

    }

    public void updateBeats()
    {
        Card curCard;

        foreach (BoardPosition position in boardPositions)
        {
            curCard = position.unitCard;
            if (curCard != null)
            {
                curCard.beatsRemaining--;
                if (curCard.beatsRemaining < 0)
                    curCard.beatsRemaining = curCard.beatSpeed;

                curCard.hasMoved = false;
            }
        }
    }

    public void movePhase()
    {
        Card curCard;

        foreach (BoardPosition position in boardPositions)
        {
            curCard = position.unitCard;
            if (curCard != null)
            {
                attemptMove(position, curCard);
            }
        }



    }

    public void attemptMove(BoardPosition bpos, Card activeCard)
    {
        int row, col;
        BoardPosition targetPosition;

        if (activeCard.beatsRemaining == 0 && !activeCard.hasMoved)
        {
            row = activeCard.row;
            col = activeCard.col;

            row += activeCard.dir;
            targetPosition = getBoardPosition(row, col);

            if (targetPosition != null)
            {
                if(targetPosition.unitCard == null)
                {
                    bpos.unitCard = null;
                    activeCard.row = row;
                    targetPosition.unitCard = activeCard;
                    activeCard.hasMoved = true;
                    activeCard.slerpTo = targetPosition.transform;
                }
            }
        }
    }

    public void damagePhase()
    {
        Card curCard;

        foreach (BoardPosition position in boardPositions)
        {
            curCard = position.unitCard;
            if (curCard != null)
            {
                if(curCard.curHealth <= 0)
                {
                    position.unitCard = null;
                    Destroy(curCard.gameObject);
                }
            }
        }
    }

    public void attackPhase()
    {
        Card curCard;

        foreach (BoardPosition position in boardPositions) 
        {
            curCard = position.unitCard;
            if (curCard != null)
            {
                attack(curCard);
            }
        }
    }

    public void attack(Card activeCard)
    {
        int row, col;
        BoardPosition targetPosition;

        if (activeCard.beatsRemaining == 0)
        {
            row = activeCard.row;
            col = activeCard.col;

            row += activeCard.unitRange;

            targetPosition = getBoardPosition(row, col);

            if (targetPosition != null && targetPosition.unitCard != null)
            {
                targetPosition.unitCard.applyDamage(activeCard.baseAttack);
            }
        }
    }

    public bool isBeat()
    {
        if ((lastTime + beatGap) <= Time.time)
        {
            lastTime = Time.time;
            return true;
        }
        else
            return false;
    }

    public BoardPosition getBoardPosition(int row, int col)
    {
        Dictionary<int, BoardPosition> outValue;
        BoardPosition position = null;

        if (indexedBoardPosition.TryGetValue(row, out outValue))
        {
            if(outValue.TryGetValue(col, out position))
            {
                //Debug.Log("Value at " + row + " " + col + "Found");
            }
        }

        return position;
    }



    public class ActionPosition
    {
        int targetRow;
        int targetCol;

        List<Card> cardsQueued = new List<Card>(); //Actions will be applied to all cards queued at once

    }

}


