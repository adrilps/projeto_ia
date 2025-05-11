using UnityEngine;

public class BlinkyGizmos : EnemyGizmos
{
    protected override void Awake()
    {
        base.Awake();
        gizmosColor = Color.red;
    }
} 