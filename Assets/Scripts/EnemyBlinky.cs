using UnityEngine;

public class EnemyBlinky : EnemyChase
{
    protected override Vector2 GetTargetPosition()
    {
        // Blinky targets Pacman's current position directly
        return enemy.target.position;
    }
} 