using UnityEngine;

public class PinkyGizmos : EnemyGizmos
{
    protected override void Awake()
    {
        base.Awake();
        gizmosColor = new Color(1f, 0.41f, 0.7f); // Pink
    }
} 