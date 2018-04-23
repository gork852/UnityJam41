using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedKeyPress : MonoBehaviour {

    public KeyCode code;   //key associated with press
    public string codeString;
    public float pressGracePerfect;
    public float pressGraceGood;
    public float pressGracePoor;

    public AudioSource beatsound;

    public beatIndicator indicator;
    public float scaler;
    public float beatGap;
    public int iters;

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

    public void resetValues()
    {

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
            if (scaler > 0)
                scaler -= Time.deltaTime/beatGap/iters;

            indicator.outerScale = scaler;

            float timeDiff = expectedTime - Time.time;

            if (Mathf.Abs(timeDiff) < pressGracePerfect)
            {
                indicator.colorAndAlpha.r = 0;
                indicator.colorAndAlpha.g = 1f;
            }
            else if (Mathf.Abs(timeDiff) < pressGraceGood)
            {
                indicator.colorAndAlpha.r = .3f;
                indicator.colorAndAlpha.g = .7f;
            }
            else if (Mathf.Abs(timeDiff) < pressGracePoor)
            {
                indicator.colorAndAlpha.r = .5f;
                indicator.colorAndAlpha.g = .5f;
            }
            else
            {
                indicator.colorAndAlpha.r = 1;
                indicator.colorAndAlpha.g = 0;
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
        else
            indicator.outerScale = 0;

        //Debug.Log("Press Result recorded: " + pressStatus);

    }

    public void setColumnKeyCode(int col)
    {
        if (!isAI)
        {
            if (col == 0)
            {
                code = KeyCode.T;
                codeString = "T";
                indicator.displayChar = "T";
            }
            else if (col == 1)
            {
                code = KeyCode.R;
                codeString = "R";
                indicator.displayChar = "R";
            }
            else if (col == 2)
            {
                code = KeyCode.E;
                codeString = "E";
                indicator.displayChar = "E";
            }
            else if (col == 3)
            {
                code = KeyCode.W;
                codeString = "W";
                indicator.displayChar = "W";
            }
            else
            {
                code = KeyCode.Q;
                codeString = "Q";
                indicator.displayChar = "Q";
            }
        }
    }

}
