using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHandPicker : MonoBehaviour {
    public GameObject camObject;
    private Camera cam;
    private CardHolder hand;
    private Card selectify;
    public GameObject selectShow;
	// Use this for initialization
	void Start () {
        cam = camObject.GetComponent<Camera>();
        hand = this.GetComponent<CardHolder>();
        selectify = null;
        selectShow = Instantiate(selectShow);
        selectShow.SetActive(false);
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
            if (selectify)
            {
                Debug.DrawLine(selectify.transform.position, interfaceHit.transform.position);
                selectShow.SetActive(true);
                selectShow.transform.position = selectify.transform.position;
                selectShow.transform.rotation = selectify.transform.rotation;
                selectShow.transform.parent = selectify.transform;
                selectShow.transform.localPosition -= new Vector3(0,0,.005f);
            }
            else
            {
                selectShow.SetActive(false);
            }
        }
        
    }
}
