using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpUI : MonoBehaviour {
    public Board b;
    public UnityEngine.UI.Text you;
    public UnityEngine.UI.Text them;
    private static int youHp = 20;
    private static int themHp = 20;
    public GameObject winG;
    public GameObject loseG;

    private bool win = false;
    private bool lose = false;
    // Use this for initialization
    void Start () {
        winG.SetActive(false);
        loseG.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        you.text = "Your HP: " + youHp;
        them.text = "Enemy HP: " + themHp;
        if (themHp < 1 && !lose)
        {
            win = true;
        }
        if (youHp < 1 && !win)
        {
            lose = true;
        }
        if (win)
            winG.SetActive(true);
        if (lose)
            loseG.SetActive(true);
	}
    public static void changeHp(float dir, int damage)
    {
        if (dir > 0)
        {
            youHp -= damage;
        }
        else
        {
            themHp -= damage;
        }
    }
}
