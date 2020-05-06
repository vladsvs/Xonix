using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float timeUpdateMovements = .03f;
    public float percentOfWaterCapture = 75;

    public int amountWaterEnemy = 2;

    private float time;

    public StatusLine statusLine;

    private Player player;
    private Enemies enemy;

    private void Start()
    {
        player = new Player();
        enemy = new Enemies();

        enemy.Init(amountWaterEnemy);

    }

    void Update()
    {
        time += Time.deltaTime;
        if ( time >= timeUpdateMovements)
        {
            GameUpdate();
            time = 0;
        }
    }

    private void GameUpdate()
    {
        enemy.Move();

        if (GameOver.gameOver) return;

        player.Move(); 

        statusLine.UpdateLine();

        if (player.IsSelfCrosed() || enemy.IsHitTrackOrPlayer())
        {
            player.DecreaseCountLives();
            if (Player.GetCountLives() > 0)
            {
                player.Reset();
                Field.ClearTrack();
            }

            else
            {
                statusLine.UpdateLine();
                Field.ClearTrack();
                GameOver.SetGameOver(true);
            }
        }
        if (Field.GetCurrentPercent() >= percentOfWaterCapture)
        {
            Field.Reset();
            enemy.Reset();
            enemy.AddEnemyWater();
            player.Reset();
        }
    }

    public void Restart()
    {
        Field.Reset();
        enemy.ResetAmount(amountWaterEnemy);
        player.Reset();
        player.ResetCountLives();
    }
}
