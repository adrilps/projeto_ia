using UnityEngine;

public class EnemyScatter : EnemyBehavior
{
    private void OnDisable()
    {
        if (enemy != null && enemy.chase != null)
        {
            enemy.chase.Enable();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        // Do nothing while the enemy is frightened
        if (node != null && enabled && !enemy.frightened.enabled)
        {
            // Pick a random available direction
            int index = Random.Range(0, node.availableDirections.Count);

            // Prefer not to go back the same direction so increment the index to
            // the next available direction
            if (node.availableDirections.Count > 1 && node.availableDirections[index] == -enemy.movement.direction)
            {
                index++;

                // Wrap the index back around if overflowed
                if (index >= node.availableDirections.Count) {
                    index = 0;
                }
            }

            enemy.movement.SetDirection(node.availableDirections[index]);
        }
    }

}
