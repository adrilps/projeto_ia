using UnityEngine;
using System.Collections.Generic;

public class EnemyGizmos : MonoBehaviour
{
    [SerializeField] protected Color gizmosColor = Color.red;
    [SerializeField] private float gizmosRadius = 0.2f;
    [SerializeField] private bool showPath = true;
    [SerializeField] private bool showTarget = true;

    private Enemy enemy;
    private List<Node> currentPath;

    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public void SetPath(List<Node> path)
    {
        currentPath = path;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || enemy == null)
            return;

        // Desenha o caminho atual do fantasma
        if (showPath && currentPath != null && currentPath.Count > 1)
        {
            Gizmos.color = gizmosColor;
            for (int i = 0; i < currentPath.Count - 1; i++)
            {
                Gizmos.DrawLine(
                    currentPath[i].transform.position,
                    currentPath[i + 1].transform.position
                );
            }
        }

        // Desenha o alvo do fantasma
        if (showTarget && enemy.target != null)
        {
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(enemy.target.position, gizmosRadius);
        }
    }
} 