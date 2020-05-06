using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Main main;

    void Update()
    {
        if (GameOver.gameOver)
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                GameOver.SetGameOver(false);
                main.Restart();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SetDirection(-1, 0);

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SetDirection(1, 0);

        else if (Input.GetKeyDown(KeyCode.UpArrow))
            SetDirection(0, 1);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            SetDirection(0, -1);
    }

    private void SetDirection(int xDirection, int yDirection)
    {
        Player.directionMovements.Set(xDirection, yDirection);
    }
}
