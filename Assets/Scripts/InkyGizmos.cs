using UnityEngine;

public class InkyGizmos : EnemyGizmos
{
    protected override void Awake()
    {
        base.Awake();
        gizmosColor = Color.cyan;
    }
} 