using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatChange : MonoBehaviour {

    public float scaleTarget;
    public float minScale;
    public float scaleBeatModifier;
    public float decay;
    float maxScale;

	// Use this for initialization
	void Start () {
        minScale = 1;
        maxScale = minScale + scaleBeatModifier;
        scaleTarget = 1;

        Board board = GameObject.FindObjectOfType<Board>();

        if (board != null)
        {
            board.Beat.AddListener(beat);
        }

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 diff = this.transform.localScale - new Vector3(1,1,1)*scaleTarget;
        this.transform.localScale = this.transform.localScale - diff * Time.deltaTime;

        scaleTarget *= decay;
        if (scaleTarget < minScale)
            scaleTarget = minScale;

    }

    public void beat()
    {
        scaleTarget += scaleBeatModifier;
        if (scaleTarget > maxScale)
            scaleTarget = maxScale;


    }


}
