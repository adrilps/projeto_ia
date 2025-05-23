using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public readonly List<Vector2> availableDirections = new();

    public List<Node> neighbors = new();
    private void Start()
    {
        availableDirections.Clear();

        // We determine if the direction is available by box casting to see if
        // we hit a wall. The direction is added to list if available.
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);

        
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1f, obstacleLayer);

        // If no collider is hit then there is no obstacle in that direction
        if (hit.collider == null) {
            availableDirections.Add(direction);
        }
    }
    /*private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        foreach(Node neighbor in neighbors){
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }*/
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        foreach (Vector2 direction in availableDirections)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction);
        }
    }*/ 
}
