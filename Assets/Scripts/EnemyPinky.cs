using UnityEngine;

public class EnemyPinky : EnemyChase
{
    protected override Vector2 GetTargetPosition()
    {
        // Pinky targets 4 tiles ahead of Pacman
        Vector2 targetPosition = enemy.target.position;
        Vector2 direction = enemy.target.GetComponent<Movement>().direction;
        
        // Multiply by 4 to get 4 tiles ahead
        targetPosition += direction * 4f;
        
        return targetPosition;
    }
} 