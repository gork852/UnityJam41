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
	}
	
	// Update is called once per frame
	void Update () {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit interfaceHit = new RaycastHit();
        Physics.Raycast(mouseRay, out interfaceHit);
        if (interfaceHit.collider)
        {
            //Debug.DrawLine(new Vector3(0, 0, 0), interfaceHit.transform.position);
            Card rayCard = interfaceHit.collider.GetComponent<Card>();
            BoardPosition bordComp = interfaceHit.collider.GetComponent<BoardPosition>();
            if (Input.GetMouseButtonDown(0) && rayCard)
            {
                selectify = rayCard;
            }
            if (selectify!=null)
            {
                Debug.DrawLine(selectify.transform.position, interfaceHit.transform.position);
                selectShow.SetActive(true);
                selectShow.transform.position = selectify.transform.position;
                selectShow.transform.rotation = selectify.transform.rotation;
                //selectShow.transform.parent = selectify.transform;
                selectShow.transform.position -= selectify.transform.forward*.005f;
                
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
}
