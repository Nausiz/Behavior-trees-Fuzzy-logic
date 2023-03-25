using System.Collections.Generic;
public class SequenceNode : Node
{
    private List<Node> children = new List<Node>();

    public void AddChild(Node child)
    {
        children.Add(child);
    }

    public override bool Evaluate()
    {
        foreach (Node child in children)
        {
            if (!child.Evaluate())
            {
                return false;
            }
        }

        return true;
    }
}