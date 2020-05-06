using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLand : Enemy
{
    public override void Init()
    {
        SetFieldType(Field.water);
        SetRandomEnemyPosition(Field.land);
        SetRandomDirection();
        SetColor(Colors.enemyLand);
        SetColorBack(Colors.land);
    }
}
