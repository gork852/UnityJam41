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
        Debug.Log(this.transform.GetComponent<LineRenderer>());
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
        }
        if (interfaceHit.collider)
        {
            //Debug.DrawLine(new Vector3(0, 0, 0), interfaceHit.transform.position);
            Card rayCard = interfaceHit.collider.GetComponent<Card>();
            BoardPosition bordComp = interfaceHit.collider.GetComponent<BoardPosition>();
            if (Input.GetMouseButtonDown(0) && rayCard != null)
            {
                if(hand.hand.Contains(rayCard))
                    selectify = rayCard;
            }
            else if (Input.GetMouseButtonDown(0) && bordComp != null)
            {
                hand.board.addCardToBoard(hand.removeCard(selectify), bordComp.row, bordComp.col, hand.dir);
                selectify = null;
            }
            if (selectify!=null)
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
            else
            {
                selectShow.SetActive(false);
            }
        }
        
        
    }
    void OnApplicationQuit()
    {
        Debug.Log("cleaning line bug");
        actionLine.positionCount = 0;
        Destroy(actionLine);
    }
}
