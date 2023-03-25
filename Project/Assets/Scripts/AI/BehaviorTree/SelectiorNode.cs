using System.Collections.Generic;

public class SelectorNode : Node
{
    public List<Node> children = new List<Node>();

    public override bool Evaluate()
    {
        foreach (Node child in children)
        {
            if (child.Evaluate())
            {
                return true;
            }
        }
        return false;
    }
}