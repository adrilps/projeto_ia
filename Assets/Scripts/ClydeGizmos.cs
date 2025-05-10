using UnityEngine;

public class ClydeGizmos : EnemyGizmos
{
    protected override void Awake()
    {
        base.Awake();
        gizmosColor = new Color(1f, 0.5f, 0f); // Orange
    }
} 