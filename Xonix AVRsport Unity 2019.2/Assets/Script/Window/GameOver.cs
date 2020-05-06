using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverText;
    public static GameObject sGameOverText;

    public static bool gameOver;

    public void Start()
    {
        sGameOverText = gameOverText;
    }

    public static void SetGameOver(bool b)
    {
        sGameOverText.SetActive(b);
        gameOver = b;
    }

}
