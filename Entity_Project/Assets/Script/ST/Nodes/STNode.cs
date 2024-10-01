using System.Collections.Generic;

namespace Script.ST
{
    using Actor;
    
    public abstract class STNode
    {
        public enum NodeType
        {
            None,
            Idle,
            Move,
            Jump
        }

        protected STNode(Actor actor)
        {
            _actor = actor;
        }

        private Actor _actor;

        public bool Active { private set; get; }
        private readonly List<STNode> _changeAbleNodes = new List<STNode>();

        protected NodeType NT { set; get; }
        public abstract void Update();

        public void RegisterChangeAbleNodeList(STNode node)
        {
            _changeAbleNodes.Add(node);
        }

        public STNode Change(NodeType nodeType)
        {
            foreach (var stNode in _changeAbleNodes)
            {
                if (stNode.NT != nodeType) 
                    continue;

                Active = true;
                return stNode;
            }

            return null;
        }
    }
}