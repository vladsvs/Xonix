using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies
{
    //private static  List<EnemyWater> enemyWater = new List<EnemyWater>();
    // private static List<EnemyLand> enemyLand = new List<EnemyLand>();


    private static List<Enemy> enemies = new List<Enemy>();

    public void Init(int amountWaterEnemy)
    {
        for (int i = 0; i < amountWaterEnemy; i++)
        {
            enemies.Add(new EnemyWater());
        }
        enemies.Add(new EnemyLand());
    }

    public void Move()
    {
        for (int i = 0; i < enemies.Count; i++)
            enemies[i].Move();
    }

    public void Reset()
    {
        ResetAmount(enemies.Count);
    }

    public void AddEnemyWater()
    {
        enemies.Add(new EnemyWater());
    }

    public void AddEnemyLand()
    {
        enemies.Add(new EnemyLand());
    }

    public bool IsHitTrackOrPlayer()
    {
        for (int i = 0; i < enemies.Count; i++)
            if (enemies[i].IsHitTrackOrPlayer())
                return true;
        return false;
    }

    public static bool IsEnemy(Vector2Int v)
    {
        for (int i = 0; i < enemies.Count; i++)
            if (v == enemies[i].Get())
                return true;
        return false;
    }

    public static int GetCount()
    {
        return enemies.Count;
    }

    public static Vector2Int GetEnemyPos(int i)
    {
        return enemies[i].Get();
    }

    public void ResetAmount(int amountEnemy)
    {
        enemies.Clear();
        Init(amountEnemy);
    }
}
