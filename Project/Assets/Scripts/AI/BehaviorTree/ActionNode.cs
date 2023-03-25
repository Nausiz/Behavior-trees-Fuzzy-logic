using System;

public class ActionNode : Node
{
    private Action<object[]> action;
    private object[] args;

    public ActionNode(Action<object[]> action, params object[] args)
    {
        this.action = action;
        this.args = args;
    }

    public override bool Evaluate()
    {
        action(args);
        return true;
    }
}