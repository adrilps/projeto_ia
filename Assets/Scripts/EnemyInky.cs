using UnityEngine;

public class EnemyInky : EnemyChase
{
    public Enemy blinky; // Reference to Blinky

    protected override Vector2 GetTargetPosition()
    {
        // Inky's target is based on Blinky's position and a point ahead of Pacman
        Vector2 pacmanPosition = enemy.target.position;
        Vector2 pacmanDirection = enemy.target.GetComponent<Movement>().direction;
        
        // Get point 2 tiles ahead of Pacman
        Vector2 pointAhead = pacmanPosition + (pacmanDirection * 2f);
        
        // Get vector from Blinky to point ahead
        Vector2 blinkyToPoint = pointAhead - (Vector2)blinky.transform.position;
        
        // Double the vector length
        Vector2 targetPosition = pointAhead + blinkyToPoint;
        
        return targetPosition;
    }
} 