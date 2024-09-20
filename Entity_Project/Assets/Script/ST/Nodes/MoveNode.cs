namespace Script.ST.Nodes
{
    using Actor;

    public class MoveNode : STNode
    {
        public MoveNode(Actor actor) : base(actor)
        {
            NT = NodeType.Move;
        }

        public override void Update()
        {
        }
    }
}