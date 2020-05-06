using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWater : Enemy
{
    public override void Init()
    {
        SetFieldType(Field.land);
        SetRandomEnemyPosition(Field.water);
        SetRandomDirection();
        SetColor(Colors.enemy);
        SetColorBack(Colors.water);
    }
}
