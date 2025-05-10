using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBehavior
{
    private Node lastNode;
    private float timeAtLastNode;
    private const float MAX_TIME_AT_NODE = 0.5f; // Tempo máximo em segundos que o fantasma pode ficar no mesmo nó
    private EnemyGizmos gizmos;
    [SerializeField] private bool useGreedySearch = false; // Flag para escolher o algoritmo de busca
    private float lastPathUpdateTime;
    private const float PATH_TIMEOUT = 1f; // Tempo em segundos para limpar o caminho se não houver atualizações

    private void OnDisable()
    {
        if (enemy != null && enemy.scatter != null)
        {
            enemy.scatter.Enable();
        }

        // Limpa o caminho quando desabilita o comportamento
        if (gizmos != null)
        {
            gizmos.SetPath(null);
        }
    }

    private void Start()
    {
        gizmos = GetComponent<EnemyGizmos>();
        if (gizmos == null)
        {
            Debug.LogWarning($"EnemyGizmos não encontrado em {gameObject.name}. Os caminhos não serão visualizados.");
        }
    }

    private void Update()
    {
        // Verifica se o fantasma está preso em um nó
        if (lastNode != null && Time.time - timeAtLastNode > MAX_TIME_AT_NODE)
        {
            // Tenta encontrar uma direção aleatória disponível
            Vector2[] availableDirections = GetAvailableDirections();
            if (availableDirections.Length > 0)
            {
                Vector2 randomDirection = availableDirections[Random.Range(0, availableDirections.Length)];
                enemy.movement.SetDirection(randomDirection);
                lastNode = null; // Reseta o último nó
            }
        }

        // Limpa o caminho se não houver atualizações
        if (gizmos != null && Time.time - lastPathUpdateTime > PATH_TIMEOUT)
        {
            gizmos.SetPath(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled || enemy.frightened.enabled)
            return;

        Node currentNode = other.GetComponent<Node>();
        if (currentNode == null)
            return;

        // Atualiza o último nó e o tempo
        if (lastNode != currentNode)
        {
            lastNode = currentNode;
            timeAtLastNode = Time.time;
        }

        Vector2 targetPosition = GetTargetPosition();
        Node targetNode = GetNearestNode(targetPosition);
        if (targetNode == null || currentNode == targetNode)
            return;

        List<Node> path = useGreedySearch ? 
            GreedySearch.FindPath(currentNode, targetNode) : 
            AStar.FindPath(currentNode, targetNode);

        if (path != null && path.Count > 1)
        {
            Vector2 direction = GetCardinalDirection(currentNode.transform.position, path[1].transform.position);
            enemy.movement.SetDirection(direction);

            // Atualiza o caminho no gizmos
            if (gizmos != null)
            {
                gizmos.SetPath(path);
                lastPathUpdateTime = Time.time; // Atualiza o timestamp da última atualização
            }
        }
        else
        {
            // Se não encontrou um caminho, limpa o caminho atual
            if (gizmos != null)
            {
                gizmos.SetPath(null);
            }
        }
    }

    private Vector2[] GetAvailableDirections()
    {
        List<Vector2> directions = new List<Vector2>();
        Node currentNode = lastNode;

        if (currentNode != null)
        {
            // Verifica cada direção possível
            if (currentNode.availableDirections.Contains(Vector2.up))
                directions.Add(Vector2.up);
            if (currentNode.availableDirections.Contains(Vector2.down))
                directions.Add(Vector2.down);
            if (currentNode.availableDirections.Contains(Vector2.left))
                directions.Add(Vector2.left);
            if (currentNode.availableDirections.Contains(Vector2.right))
                directions.Add(Vector2.right);
        }

        return directions.ToArray();
    }

    protected virtual Vector2 GetTargetPosition()
    {
        var objects = FindObjectsByType<Character>(FindObjectsSortMode.None);
        if (objects.Length > 0)
        {
            return objects[0].transform.position;
        }
        return Vector2.zero;
    }

    private Node GetNearestNode(Vector2 position)
    {
        Node[] allNodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
        Node closest = null;
        float minDistance = float.MaxValue;

        foreach (Node node in allNodes)
        {
            float dist = Vector2.Distance(position, node.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = node;
            }
        }

        return closest;
    }

    private Vector2 GetCardinalDirection(Vector2 from, Vector2 to)
    {
        Vector2 diff = to - from;

        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            return diff.x > 0 ? Vector2.right : Vector2.left;
        else
            return diff.y > 0 ? Vector2.up : Vector2.down;
    }
}
