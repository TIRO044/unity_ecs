using System.Collections.Generic;

namespace Script.ST
{
    public class StateMachine
    {
        private readonly LinkedList<STNode> _nodes = new();
        
        public void Init(STNode rootNode)
        {
            _nodes.AddLast(rootNode);
        }

        public void ActiveNode(STNode.NodeType nodeType)
        {
            // 아 너무 복잡해지는데
            
            
            foreach (var node in _nodes)
            {
                var changedNode = node.Change(nodeType);
                if (changedNode != null)
                {
                    _nodes.AddLast(changedNode);
                    break;
                }
            }
        }

        public void UpdateNodes()
        {
            foreach (var node in _nodes)
            {
                if (node.Active)
                    node.Update();
            }
        }

        public STNode CurrentNode()
        {
            if (_nodes.Count == 0)
            {
                return null;
            }
            
            return _nodes.Last.Value;
        }
    }
}