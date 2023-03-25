public class BehaviorTree
{
    private Node rootNode;

    public void SetRoot(Node rootNode)
    {
        this.rootNode = rootNode;
    }

    public void Tick()
    {
        if (rootNode != null)
        {
            rootNode.Evaluate();
        }
    }
}