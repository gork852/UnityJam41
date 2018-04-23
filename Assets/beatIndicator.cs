using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatIndicator : MonoBehaviour {
    public string displayChar;
    private string dispChar;
    private UnityEngine.UI.Text tex;
    public float outerScale;
    public GameObject outer;
    private UnityEngine.UI.Image outImg;
    public Color colorAndAlpha;
	// Use this for initialization
	void Start () {
        dispChar = displayChar;
        outImg = outer.GetComponent<UnityEngine.UI.Image>();
        tex = this.GetComponentInChildren<UnityEngine.UI.Text>();
        tex.text = dispChar;
	}
	
	// Update is called once per frame
	void Update () {
        if (dispChar != displayChar)
        {
            tex.text = dispChar;
            dispChar = displayChar;
        }
        outImg.color = colorAndAlpha;
        outer.transform.localScale = new Vector3(outerScale+.5f, outerScale + .5f, outerScale+.5f);

        Vector3 pos = Camera.main.transform.position;
        this.transform.LookAt(pos);
    }
}
