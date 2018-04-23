using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedKeyPress : MonoBehaviour {

    public KeyCode code;   //key associated with press
    public string codeString;
    public float pressGracePerfect;
    public float pressGraceGood;
    public float pressGracePoor;

    public bool isAI;

    public float timePressed;
    public float expectedTime;
    public bool active = false;
    public bool pressed = false;

    public enum Status {Perfect, Good, Poor, Miss};

    public IEnumerator update;

    public Status pressStatus;

    public void initTimes(float gracePerfect, float graceGood, float gracePoor)
    {
        pressGracePerfect = gracePerfect;
        pressGraceGood = graceGood;
        pressGracePoor = gracePoor;
    }

    public void setAI(bool val)
    {
        isAI = val;
    }
	
	// Update is called once per frame
	void Update () {

        if (active && !isAI)
        {
            Debug.Log("Key Active! " + codeString);
            if (Input.GetKeyDown(code))
            {
                active = false;
                pressed = true;
                timePressed = Time.time;
            }
        }

	}

    public void compareTime()
    {
        float timeDiff = expectedTime - timePressed;

        Debug.Log("Time Expected: " + expectedTime + "Time Pressed: " + timePressed);

        if (Mathf.Abs(timeDiff) < pressGracePerfect)
            pressStatus = Status.Perfect;
        else if (Mathf.Abs(timeDiff) < pressGraceGood)
            pressStatus = Status.Good;
        else if (Mathf.Abs(timeDiff) < pressGracePoor)
            pressStatus = Status.Poor;
        else
            pressStatus = Status.Miss;

        if (isAI)
            pressStatus = Status.Good;

        //Debug.Log("Press Result recorded: " + pressStatus);

    }

    public void setColumnKeyCode(int col)
    {
        if (col == 0)
        {
            code = KeyCode.T;
            codeString = "T";
        }
        else if (col == 1)
        {
            code = KeyCode.R;
            codeString = "R";
        }
        else if (col == 2)
        {
            code = KeyCode.E;
            codeString = "E";
        }
        else if (col == 3)
        {
            code = KeyCode.W;
            codeString = "W";
        }
        else
        {
            code = KeyCode.Q;
            codeString = "Q";
        }
    }

}
