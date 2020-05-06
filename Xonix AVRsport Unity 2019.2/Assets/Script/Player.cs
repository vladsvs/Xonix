using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private static Vector2Int posPlayer;
    private static bool isWater, isSelfCross;
    private byte oldPos;
    private static int countLives = 3;

    private Color playerColor = Color.red;

    public static Vector2Int directionMovements;

    public Player()
    {
        posPlayer = new Vector2Int(Field.fieldSize.x / 2, Field.fieldSize.y-1);

        oldPos = Field.land;
        Field.Set(posPlayer, oldPos, Colors.player);
    }


    public void Reset()
    {
        //if (oldPos == Field.land) Field.Set(posPlayer, Field.land, Colors.land);
        //if (oldPos == Field.water) Field.Set(posPlayer, Colors.water);

        posPlayer = new Vector2Int(Field.fieldSize.x / 2, Field.fieldSize.y - 1);
        directionMovements = Vector2Int.zero;
        oldPos = Field.land;
        isSelfCross = isWater = false;

        Field.Set(posPlayer, oldPos, Colors.player);
    }

    public static Vector2Int Get()
    {
        return posPlayer;
    }

    public void Move()
    {
        if (directionMovements == Vector2Int.zero)
        {
            Field.Set(posPlayer, Colors.player);
            return;
        } 

        if (oldPos==Field.land) Field.Set(posPlayer, Field.land, Colors.land);
        if (oldPos==Field.track) Field.Set(posPlayer, Field.track, Colors.track);

        posPlayer += directionMovements;

        if (posPlayer.x < 0) { posPlayer.x = 0; directionMovements = Vector2Int.zero; }
        if (posPlayer.y < 0) { posPlayer.y = 0; directionMovements = Vector2Int.zero; }
        if (posPlayer.x > Field.fieldSize.x - 1) { posPlayer.x = Field.fieldSize.x - 1; directionMovements = Vector2Int.zero; }
        if (posPlayer.y > Field.fieldSize.y - 1) { posPlayer.y = Field.fieldSize.y - 1; directionMovements = Vector2Int.zero; }

        oldPos = Field.Get(posPlayer);
        Field.Set(posPlayer, Colors.player);

        isSelfCross = Field.Get(posPlayer) == Field.track;

        if (oldPos == Field.land && isWater)
        {
            directionMovements = Vector2Int.zero;
            isWater = false;
            Field.TryToFill();
        }
        else if (oldPos == Field.water)
        {
            isWater = true;
            oldPos = Field.Set(posPlayer, Field.track);
        }
    }

    public bool IsSelfCrosed()
    {
        return isSelfCross;
    }

    public static int GetCountLives()
    {
        return countLives;
    }

    public void DecreaseCountLives()
    {
        countLives--;
    }

    public void ResetCountLives()
    {
        countLives = 3;
    }
}
