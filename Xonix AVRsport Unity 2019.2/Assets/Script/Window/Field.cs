using System;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static readonly byte water, temp = 1, land = 2, track = 3;

    private static byte[,] field;
    private static Material[,] fieldMaterial;

    public Transform fieldQuad;

    private static int waterArea;
    private static float currentWaterArea;
    private static int countScore;

    public static Vector2Int fieldSize = new Vector2Int( 0, 46 );

    private void Awake()
    {
        Init();
    }

    private void ShowField(int quadHorizontal, int quadVertical, float quadSize)
    {
        field = new byte[quadHorizontal, quadVertical];
        fieldMaterial = new Material[quadHorizontal, quadVertical];
        fieldQuad.localScale = new Vector3(quadSize, quadSize, 1);

        for (int y = 0; y < quadVertical; y++)
        {
            for (int x = 0; x < quadHorizontal; x++)
            {
                fieldMaterial[x,y] = Instantiate(fieldQuad, new Vector3(x * quadSize, y * quadSize, 0), Quaternion.identity).GetComponent<Renderer>().material;

                if( y < 2 || x <  2 || x > quadHorizontal - 3 || y > quadVertical - 3)
                {
                    fieldMaterial[x, y].color = Colors.land;
                    field[x, y] = land;
                }
                else
                    field[x, y] = water;
            }
        }
    }

    public static float GetCurrentPercent()
    {
        return 100f - currentWaterArea / waterArea * 100;
    }

    public static int GetCountScore() { return countScore; }

    private void Init()
    {

        float statusLineSize = Camera.main.orthographicSize * 2 / 100 * StatusLine.statusLine;
        float quadSize = ( Camera.main.orthographicSize * 2 - statusLineSize ) / fieldSize.y;

        fieldSize.x = fieldSize.x != 0 ? fieldSize.x : (int)(Screen.width * fieldSize.y  / (Screen.height - StatusLine.statusLine / 100 * Screen.height));

        float cameraLeft = (quadSize * fieldSize.x - quadSize) / 2;

        Camera.main.transform.position = new Vector3(cameraLeft, Camera.main.orthographicSize  - quadSize / 2 - statusLineSize, -1);
        ShowField(fieldSize.x, fieldSize.y, quadSize);

        currentWaterArea = waterArea = (fieldSize.x - 4) * (fieldSize.y - 4);
    }

    public static byte Get(int x, int y)
    {
        if (x < 0 || y < 0 || x > fieldSize.x - 1 || y > fieldSize.y - 1) return water;
        return field[x, y];
    }

    public static byte Get(Vector2Int v)
    {
        if (v.x < 0 || v.y < 0 || v.x > fieldSize.x - 1 || v.y > fieldSize.y - 1) return water;
        return field[v.x, v.y];
    }

    public static void Set(int x, int y, byte type, Color color)
    {
        fieldMaterial[x, y].color = color;
        field[x, y] = type;
    }

    public static byte Set(Vector2Int v, byte type, Color color)
    {
        fieldMaterial[v.x, v.y].color = color;
        field[v.x, v.y] = type;
        return type;
    }

    public static void Set(int x, int y, byte type)
    {
        field[x, y] = type;
    }

    public static byte Set(Vector2Int v, byte type)
    {
        field[v.x, v.y] = type;
        return type;
    }

    public static void Set(int x, int y,Color color)
    {
        fieldMaterial[x, y].color = color;
    }

    public static void Set(Vector2Int v,Color color)
    {
        fieldMaterial[v.x, v.y].color = color;
    }

    public static void FillTemporary(int x, int y)
    {
        if (field[x, y] != water) return;
        field[x, y] = temp;
        for (int dx = -1; dx < 2; dx++)
            for (int dy = -1; dy < 2; dy++) FillTemporary(x + dx, y + dy);
    }

    public static void TryToFill()
    {
        currentWaterArea = 0;

        for (int i = 0; i < Enemies.GetCount(); i++)
            FillTemporary(Enemies.GetEnemyPos(i).x, Enemies.GetEnemyPos(i).y);

        for (int y = 0; y < fieldSize.y; y++)
            for (int x = 0; x < fieldSize.x; x++)
            {
                if (field[x,y] == track || field[x,y] == water)
                {
                    Set(x, y, land, Colors.land);
                    countScore += 10;
                }
                if (field[x,y] == temp)
                {
                    Set(x, y, water);
                    currentWaterArea++;
                }
            }
    }

    public static void ClearTrack()
    { 
        for (int y = 0; y < fieldSize.y; y++)
            for (int x = 0; x < fieldSize.x; x++)
                if (Get(x,y) == track)
                    Set(x, y, water, Colors.water);
    }

    public static void Reset()
    {
        for (int y = 0; y < fieldSize.y; y++)
        {
            for (int x = 0; x < fieldSize.x; x++)
            {
                if (y < 2 || x < 2 || x > fieldSize.x - 3 || y > fieldSize.y - 3)
                {
                    fieldMaterial[x, y].color = Colors.land;
                    field[x, y] = land;
                }
                else
                {
                    fieldMaterial[x, y].color = Colors.water;
                    field[x, y] = water;
                }  
            }
        }
        currentWaterArea = waterArea;
        countScore = 0;
    }
}
