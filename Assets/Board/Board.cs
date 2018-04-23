using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour {

    float beatGap = 1.0f;
    float lastTime;
    public GameObject NumberPrefab;
    public UnityEvent Beat;

    public List<BoardPosition> boardPositions = new List<BoardPosition>();
    Dictionary<int, Dictionary<int, BoardPosition>> indexedBoardPosition = new Dictionary<int, Dictionary<int, BoardPosition>>();

    // Use this for initialization
    void Start() {

        lastTime = Time.time;
        BoardPosition[] tempList = this.GetComponentsInChildren<BoardPosition>();

        Debug.Log("Board Postions found: " + tempList.Length);

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
            pressResultsPhase();
            actionPhase();
            damagePhase();
            attackPhase();
            damagePhase();
            movePhase();
            Beat.Invoke();
            updateBeats();
        }
        

    }

    public void pressResultsPhase()
    {
        Card curCard;

        foreach (BoardPosition position in boardPositions)
        {
            curCard = position.unitCard;
            if (curCard != null && curCard.timedPress != null)
            {
                curCard.timedPress.expectedTime = Time.time;
                curCard.timedPress.active = false;
                curCard.timedPress.compareTime();
            }
        }
    }

    public void actionPhase()
    {
        Card curCard;

        foreach (BoardPosition position in boardPositions)
        {
            curCard = position.unitCard;
            if (curCard != null && curCard.beatsRemaining == 0)
            {
                Ability[] abilities = curCard.GetComponents<Ability>();
                foreach(Ability ability in abilities)
                {
                    ability.action(this);
                }

            }
        }



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
            card.transform.parent = null;
            card.state = Card.cardState.onboard;
            if(card.type == Card.cardType.creature)
            {
                Vector3 spanwpoint = card.transform.position;
                Debug.DrawLine(spanwpoint, new Vector3());
                
                GameObject numberHold = new GameObject();
                numberHold.transform.parent = card.transform;
                numberHold.transform.localPosition = new Vector3();
                numberHold.transform.rotation = new Quaternion();
                numberHold.transform.localScale = new Vector3(1, 1, 1);
                //Debug.Break();
                
                GameObject atkNum = Instantiate(NumberPrefab);
                NumberDisplay.numberGetter atkGet = delegate { return card.baseAttack; };
                atkNum.GetComponent<NumberDisplay>().getter = atkGet;
                atkNum.transform.parent = numberHold.transform;
                atkNum.transform.localScale = new Vector3(1, 1, 1);
                atkNum.transform.localPosition = new Vector3(-.3f,-.4f,-10f);

                GameObject hpNum = Instantiate(NumberPrefab);
                NumberDisplay.numberGetter hpGet = delegate { return card.curHealth; };
                hpNum.GetComponent<NumberDisplay>().getter = hpGet;
                hpNum.transform.parent = numberHold.transform;
                hpNum.transform.localScale = new Vector3(1, 1, 1);
                hpNum.transform.localPosition = new Vector3(.3f,-.4f,-10f);

                GameObject actionNum = Instantiate(NumberPrefab);
                NumberDisplay.numberGetter actGet = delegate { return card.beatsRemaining; };
                actionNum.transform.parent = numberHold.transform;
                actionNum.transform.localScale = new Vector3(1, 1, 1);
                actionNum.transform.localPosition = new Vector3(0, 0, -10f);
            }
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

                resetCardTimer(curCard);


            }
        }
    }

    public void resetCardTimer(Card curCard)
    {
        if (curCard.beatsRemaining == 0 && curCard.state == Card.cardState.onboard)
        {
            curCard.timedPress.active = true;
            curCard.timedPress.setColumnKeyCode(curCard.col);
            curCard.timedPress.pressed = false;
            curCard.timedPress.expectedTime = 0;
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

            if (targetPosition != null && targetPosition.unitCard != null && targetPosition.unitCard.dir != activeCard.dir && activeCard.baseAttack > 0)
            {
                targetPosition.unitCard.applyDamage(activeCard.baseAttack);
                if(targetPosition.unitCard.unitRange == activeCard.unitRange)
                    activeCard.applyDamage(targetPosition.unitCard.baseAttack);
            }
        }
    }

    public bool isBeat()
    {
        if ((lastTime + beatGap) <= Time.time)
        {
            Debug.Log(Time.time);
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

    public bool isPlayableAtPosition(Card card, int row, int col)
    {
        BoardPosition pos = getBoardPosition(row, col);
        if (pos.isPlayableHere(card))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}


