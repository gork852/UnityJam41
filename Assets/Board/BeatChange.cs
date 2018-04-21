using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatChange : MonoBehaviour {

    public float scaleTarget;
    public float minScale;
    public float scaleBeatModifier;
    float maxScale;

	// Use this for initialization
	void Start () {
        minScale = 1;
        maxScale = minScale + scaleBeatModifier;
        scaleTarget = 1;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 diff = this.transform.localScale - new Vector3(1,1,1)*scaleTarget;
        this.transform.localScale = this.transform.localScale - diff * Time.deltaTime;

        scaleTarget *= .87f;
        if (scaleTarget < 1)
            scaleTarget = 1;

    }

    public void beat()
    {
        scaleTarget += scaleBeatModifier;
        if (scaleTarget > maxScale)
            scaleTarget = maxScale;


    }


}
