using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GreedySearch
{
    public static List<Node> FindPath(Node start, Node target)
    {
        // Lista de nós a serem explorados
        List<PathNode> openList = new();
        
        // Conjunto de nós já visitados
        HashSet<Node> closedSet = new();

        // Adiciona o nó inicial à lista aberta
        PathNode startPathNode = new(start);
        startPathNode.hCost = Vector2.Distance(start.transform.position, target.transform.position);
        openList.Add(startPathNode);

        while (openList.Count > 0)
        {
            // Seleciona o nó com menor H (distância até o alvo)
            PathNode current = openList.OrderBy(n => n.hCost).First();

            // Se o nó atual é o destino, reconstruímos o caminho
            if (current.node == target)
            {
                List<Node> finalPath = ReconstructPath(current);

                // Log para debug
                /*Debug.Log("Caminho encontrado (Greedy):");
                foreach (Node node in finalPath)
                {
                    Debug.Log($"→ {node.name} ({node.transform.position})");
                }*/

                return finalPath;
            }

            openList.Remove(current);
            closedSet.Add(current.node);

            // Avalia todos os vizinhos do nó atual
            foreach (Node neighbor in current.node.neighbors)
            {
                if (closedSet.Contains(neighbor))
                    continue;

                // Verifica se o vizinho já está na lista aberta
                PathNode neighborNode = openList.FirstOrDefault(n => n.node == neighbor);

                if (neighborNode == null)
                {
                    // Se for a primeira vez que o vizinho é encontrado, adiciona à lista
                    neighborNode = new PathNode(neighbor)
                    {
                        hCost = Vector2.Distance(neighbor.transform.position, target.transform.position),
                        parent = current
                    };
                    openList.Add(neighborNode);
                }
            }
        }

        // Se a lista foi esvaziada e nenhum caminho foi encontrado
        Debug.LogWarning("Nenhum caminho foi encontrado (Greedy).");
        return null;
    }

    // Reconstrói o caminho final, do destino até a origem
    private static List<Node> ReconstructPath(PathNode endNode)
    {
        List<Node> path = new();
        PathNode current = endNode;

        while (current != null)
        {
            path.Add(current.node);
            current = current.parent;
        }

        path.Reverse(); // Inverte para começar no início
        return path;
    }
} 