using System;
using UnityEngine;
using UnityEngine.UI;

public class StatusLine : MonoBehaviour
{
    public Text score, xn, full;
    public static float statusLine = 5;

    void Start()
    {
        UpdateLine();
    }

    public void UpdateLine()
    {
        score.text = String.Format("Score: {0:N0}" , Field.GetCountScore());
        full.text = String.Format("Full: {0}%", Mathf.FloorToInt(Field.GetCurrentPercent()));
        xn.text = String.Format("Xn: {0}", Player.GetCountLives());
    }
}

