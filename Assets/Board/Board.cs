using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    float beatGap = 1.0f;
    float lastTime = Time.time;

    List<BoardPosition> boardPositions = new List<BoardPosition>();
    Dictionary<int, Dictionary<int, ActionPosition>> queuedAttackActions = new Dictionary<int, Dictionary<int, ActionPosition>>();
    Dictionary<int, Dictionary<int, ActionPosition>> queuedMoveActions = new Dictionary<int, Dictionary<int, ActionPosition>>();

    // Use this for initialization
    void Start() {

        this.GetComponentsInChildren<BoardPosition>();



    }

    // Update is called once per frame
    void Update() {
        int row, col;

        if (isBeat())
        {

            foreach(BoardPosition position in boardPositions)
            {
                row = position.unitCard.row;
                col = position.unitCard.col;





            }



        }
    }




    public bool isBeat()
    {
        if (lastTime + beatGap <= Time.time)
        {
            lastTime = Time.time;
            return true;
        }
        else
            return false;
    }





    public class ActionPosition
    {
        public string action;
        int targetRow;
        int targetCol;

        List<Card> cardsQueued = new List<Card>(); //Actions will be applied to all cards queued at once

    }

}


