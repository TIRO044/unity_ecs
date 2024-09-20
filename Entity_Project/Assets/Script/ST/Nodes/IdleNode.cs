namespace Script.ST.Nodes
{
    using Actor;

    public class IdleNode : STNode
    {
        public IdleNode(Actor actor) : base(actor)
        {
            NT = NodeType.Idle;
        }
        
        public override void Update()
        {
        }
    }
}