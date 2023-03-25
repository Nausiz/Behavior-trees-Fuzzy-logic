using System;

public class ConditionNode : Node
{
    private Func<object[], bool> condition;
    private object[] args;

    public ConditionNode(Func<object[], bool> condition, params object[] args)
    {
        this.condition = condition;
        this.args = args;
    }

    public override bool Evaluate()
    {
        return condition(args);
    }
}