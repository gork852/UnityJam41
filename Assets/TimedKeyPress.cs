using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedKeyPress : MonoBehaviour {

    public KeyCode code;   //key associated with press
    public string codeString;
    public float pressGracePerfect;
    public float pressGraceGood;
    public float pressGracePoor;


    float timePressed;
    public float expectedTime;
    public bool active = false;
    public bool pressed = false;

    public enum Status {Perfect, Good, Poor, Miss};

    public IEnumerator update;

    Status pressStatus;

    public void initTimes(float gracePerfect, float graceGood, float gracePoor)
    {
        pressGracePerfect = gracePerfect;
        pressGraceGood = graceGood;
        pressGracePoor = gracePoor;
        
    }

	
	// Update is called once per frame
	void Update () {

        if (active)
        {

            Debug.Log("Key Active! " + codeString);
            if (Input.GetKeyDown(code))
            {
                compareTime();
                active = false;
                pressed = true;
            }
        }

	}

    public void compareTime()
    {
        float timeDiff = expectedTime - timePressed;
        
        if (Mathf.Abs(timeDiff) < pressGracePerfect)
            pressStatus = Status.Perfect;
        else if (Mathf.Abs(timeDiff) < pressGraceGood)
            pressStatus = Status.Good;
        else if (Mathf.Abs(timeDiff) < pressGracePoor)
            pressStatus = Status.Poor;
        else
            pressStatus = Status.Miss;

        Debug.Log("Press Result recorded: " + pressStatus);

    }

    public void setColumnKeyCode(int col)
    {
        if (col == 0)
        {
            code = KeyCode.Q;
            codeString = "Q";
        }
        else if (col == 1)
        {
            code = KeyCode.W;
            codeString = "W";
        }
        else if (col == 2)
        {
            code = KeyCode.E;
            codeString = "E";
        }
        else if (col == 3)
        {
            code = KeyCode.R;
            codeString = "R";
        }
        else
        {
            code = KeyCode.T;
            codeString = "T";
        }
    }

}
