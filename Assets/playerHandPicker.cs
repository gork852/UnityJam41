using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHandPicker : MonoBehaviour {
    public GameObject camObject;
    private Camera cam;
    private CardHolder hand;
    private Card selectify;
    public GameObject selectShow;
    private LineRenderer actionLine;
    public GameObject targetShow;
    public GameObject targetBoard;
    public GameObject targetCol;
    public GameObject targetRow;
    private Vector3 defaultHandPos;
    public Vector3 hideHandOffset;
    private Vector3 hideHandPos;
    
	// Use this for initialization
	void Start () {
        cam = camObject.GetComponent<Camera>();
        hand = this.GetComponent<CardHolder>();
        selectify = null;
        selectShow = Instantiate(selectShow);
        actionLine = selectShow.GetComponent<LineRenderer>();
        actionLine.positionCount = 20;
        selectShow.SetActive(false);
        targetShow = Instantiate(targetShow);
        targetShow.SetActive(false);
        targetBoard = Instantiate(targetBoard);
        targetBoard.SetActive(false);
        targetRow = Instantiate(targetRow);
        targetRow.SetActive(false);
        targetCol = Instantiate(targetCol);
        targetCol.SetActive(false);
        //Debug.Log(this.transform.GetComponent<LineRenderer>());
        defaultHandPos = hand.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        

        hideHandPos = defaultHandPos + hideHandOffset;

        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit interfaceHit = new RaycastHit();
        Physics.Raycast(mouseRay, out interfaceHit);
        if (Input.GetMouseButtonDown(1))
        {
            selectify = null;
        }
        if (selectify != null)
        {
            selectShow.SetActive(true);
            selectShow.transform.position = selectify.transform.position;
            selectShow.transform.rotation = selectify.transform.rotation;
            selectShow.transform.position -= selectify.transform.forward * .005f;
            actionLine.positionCount = 0;

            hand.transform.position = hand.transform.position*(1 - Time.deltaTime) + hideHandPos * Time.deltaTime;

            
        }
        else
        {
            hand.transform.position = hand.transform.position * (1 - Time.deltaTime) + defaultHandPos * Time.deltaTime;
            targetBoard.SetActive(false);
        }
        if (interfaceHit.collider)
        {
            Debug.DrawLine(new Vector3(0, 0, 0), interfaceHit.transform.position);
            Card rayCard = interfaceHit.collider.GetComponent<Card>();
            BoardPosition bordComp = interfaceHit.collider.GetComponent<BoardPosition>();
            if (selectify!=null&&selectify.type == Card.cardType.targetBoard && rayCard == null)
            {
                targetBoard.SetActive(true);
            }
            else
            {
                targetBoard.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0) && rayCard != null)
            {
                if (rayCard.state == Card.cardState.onboard && selectify!=null)
                {
                    if (rayCard.isValidTarget(selectify))
                    {
                        hand.removeCard(selectify);
                        rayCard.playCardToThis(selectify);
                        selectify = null;
                    }
                }
                if (hand.hand.Contains(rayCard))
                {
                    selectify = rayCard;
                }
            }
            else if (Input.GetMouseButtonDown(0) && bordComp != null && selectify != null)
            {
                if (bordComp.isPlayableHere(selectify))
                {
                    hand.removeCard(selectify);
                    if (selectify.type == Card.cardType.targetBoard)
                    {
                        bool applyOnce = false;
                        for (int col = 0; col <= 4; col++)
                        {
                            for(int row = 0; row <= 7; row++)
                            {
                                Debug.Log("pos" + row + ":" + col);
                                BoardPosition applypos = hand.board.getBoardPosition(row, col);
                                if (applypos.unitCard != null && applypos.unitCard.type == Card.cardType.creature)
                                {
                                    Debug.Log("attempt");
                                    selectify.GetComponent<targetAction>().actOnTarget(applypos.unitCard);
                                    applyOnce = true;
                                }
                            }
                        }
                        selectify.transform.parent = null;
                        selectify.transform.position = new Vector3(0, 2, 0);
                        if (!applyOnce)
                        {
                            Destroy(selectify);
                        }
                    }
                    else
                    {
                        hand.board.addCardToBoard(selectify, bordComp.row, bordComp.col, hand.dir);
                    }
                    selectify = null;
                }
            }
            if ((selectify!=null&&(selectify.type!=Card.cardType.targetBoard))
                &&
                ((bordComp != null && bordComp.isPlayableHere(selectify)) || (rayCard!=null&&rayCard.isValidTarget(selectify))))
            {
                if (selectify.type == Card.cardType.targetCol)
                {
                    if (bordComp != null)
                    {
                        targetCol.SetActive(true);
                        targetCol.transform.position = bordComp.transform.position - new Vector3(0, 0, bordComp.transform.position.z);
                    }
                }
                else if (selectify.type == Card.cardType.targetRow)
                {
                    if(bordComp != null)
                    {
                        targetRow.SetActive(true);
                        targetRow.transform.position = bordComp.transform.position - new Vector3(bordComp.transform.position.x, 0, 0);
                    }
                }
                else {


                    GameObject targ = bordComp != null ? bordComp.gameObject : rayCard.gameObject;
                    targetShow.SetActive(true);
                    targetShow.transform.position = targ.transform.position + targ.transform.forward * .05f;
                    if (bordComp != null)
                    {
                        targetShow.transform.rotation = new Quaternion(1, 0, 0, 1);
                    }
                    else
                    {
                        targetShow.transform.rotation = targ.transform.rotation;
                    }
                }
            }
            else
            {
                targetShow.SetActive(false);
            }
            if (selectify!=null && (
                bordComp!=null&&bordComp.isPlayableHere(selectify) ||
                rayCard!=null&&rayCard.isValidTarget(selectify)
                ))
            {
                
                actionLine.positionCount = 20;
                Debug.DrawLine(selectify.transform.position, interfaceHit.transform.position);
                for (int i = 0; i < actionLine.positionCount; i++)
                {
                    float percentLine = i / (actionLine.positionCount - 1f);
                    float leanBias = selectify.transform.position.x > 0 ? Mathf.Sin(percentLine * Mathf.PI) : -Mathf.Sin(percentLine * Mathf.PI);
                    Vector3 hoverHeight = new Vector3(leanBias * 2, Mathf.Sin(percentLine * Mathf.PI) * 4, 0);
                    actionLine.SetPosition(i, selectify.transform.position * (1 - percentLine) + hoverHeight + interfaceHit.transform.position * percentLine);
                }
            }
            else if(selectify==null)
            {
                selectShow.SetActive(false);
            }
        }
        else
        {
            targetShow.SetActive(false);
            targetBoard.SetActive(false);
        }
        
        
        
    }

    void OnApplicationQuit()
    {
        Debug.Log("cleaning line bug");
        actionLine.positionCount = 0;
        Destroy(actionLine);
    }
}
