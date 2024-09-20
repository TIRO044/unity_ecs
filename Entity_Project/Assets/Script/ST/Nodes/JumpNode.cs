namespace Script.ST.Nodes
{
    public class JumpNode : STNode
    {
        public JumpNode(Actor.Actor actor) : base(actor)
        {
            NT = NodeType.Jump;
        }

        public override void Update()
        {
        }
    }
}