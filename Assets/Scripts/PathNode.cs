public class PathNode
{
    public Node node;
    public float gCost;
    public float hCost;
    public PathNode parent;

    public float FCost => gCost + hCost;

    public PathNode(Node node)
    {
        this.node = node;
    }
}
