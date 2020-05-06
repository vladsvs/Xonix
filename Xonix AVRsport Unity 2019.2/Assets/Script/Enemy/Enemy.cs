using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2Int pos = Vector2Int.zero, dPos = Vector2Int.zero;
    private bool x, y;
    private byte fieldType;
    private Color colorEnemy, colorBack;

    protected Enemy() => Init();

    protected void SetFieldType(byte field)
    {
        fieldType = field;
    }

    protected void SetRandomEnemyPosition(byte field)
    {
        do
        {
            pos.Set(Random.Range(2, Field.fieldSize.x - 2), Random.Range(1, Field.fieldSize.y - 2));
        } while (Field.Get(pos) != field && !Enemies.IsEnemy(pos));
    }

    protected void SetRandomDirection()
    {
        dPos.Set(Random.value > .5f ? 1 : -1, Random.value > .5f ? 1 : -1);
    }

    protected void SetColor(Color color)
    {
        colorEnemy = color;
        Field.Set(pos, color);
    }

    protected void SetColorBack(Color color)
    {
        colorBack = color;
    }

    void UpdateDpos()
    {
        if (Field.Get( pos.x + dPos.x, pos.y) == fieldType) x = true;
        if (Field.Get( pos.x, pos.y + dPos.y) == fieldType) y = true;

        if (!x && !y)
            if (Field.Get(pos + dPos) == fieldType)
            {
                x = true;
                y = true;
            }

        if (x)
        {
            dPos.x = -dPos.x;
            x = false;
        }

        if (y)
        {
            dPos.y = -dPos.y;
            y = false;
        }

        if (Field.Get(pos) == fieldType) dPos = Vector2Int.zero;
    }

    public void Move()
    {
        UpdateDpos();

        Field.Set(pos, colorBack);

        pos += dPos;

        Field.Set(pos, colorEnemy);
    }

    public Vector2Int Get()
    {
        return pos;
    }

    public bool IsHitTrackOrPlayer()
    {
        UpdateDpos();
        if (Field.Get(pos + dPos) == Field.track) return true;
        if (pos + dPos == Player.Get()) return true;
        return false;
    }

    public virtual void Init()
    {
        Debug.Log("lol");
    }
}
