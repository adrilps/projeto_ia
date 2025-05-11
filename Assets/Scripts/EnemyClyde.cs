using UnityEngine;

public class EnemyClyde : EnemyChase
{
    public Vector2 scatterCorner = new Vector2(-8f, -8f); // Clyde's scatter corner
    public float distanceThreshold = 8f; // Distance at which Clyde switches to scatter mode

    protected override Vector2 GetTargetPosition()
    {
        // Calculate distance to Pacman
        float distanceToPacman = Vector2.Distance(transform.position, enemy.target.position);
        
        // If Clyde is too close to Pacman, he'll target his scatter corner
        if (distanceToPacman < distanceThreshold)
        {
            return scatterCorner;
        }
        
        // Otherwise, he'll target Pacman directly
        return enemy.target.position;
    }
} 