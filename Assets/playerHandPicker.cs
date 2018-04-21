using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHandPicker : MonoBehaviour {
    public GameObject camObject;
    private Camera cam;
    private CardHolder hand;
	// Use this for initialization
	void Start () {
        cam = camObject.GetComponent<Camera>();
        hand = this.GetComponent<CardHolder>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit interfaceHit = new RaycastHit();
        Physics.Raycast(mouseRay, out interfaceHit);
        if (interfaceHit.collider)
        {
            Debug.DrawLine(new Vector3(0, 0, 0), interfaceHit.transform.position);
            
        }
    }
}
