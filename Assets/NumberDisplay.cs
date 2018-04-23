using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDisplay : MonoBehaviour {
    public int number = 0;
    public Color color;
    public delegate int numberGetter();
    public numberGetter getter=null;
    private Color oldColor;
    private int oldNumber = 0;
    private string numString;
    private UnityEngine.UI.Text text;
	// Use this for initialization
	void Start () {
        text = this.GetComponentInChildren<UnityEngine.UI.Text>();
        numString = number.ToString();
        text.text = numString;
        oldColor = color;
        text.color = color;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(getter!=null) number = getter();
        if (number != oldNumber)
        {
            oldNumber = number;
            numString = number.ToString();
            text.text = numString;
        }
        if (color != oldColor)
        {
            color = oldColor;
            text.color = color;
        }

        Vector3 pos = Camera.main.transform.position;
        this.transform.LookAt(pos);
	}
}
