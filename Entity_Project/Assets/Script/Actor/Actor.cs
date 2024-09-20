using Script.EventHandler;
using Script.ST;
using Script.ST.Nodes;
using UnityEngine;
using StateMachine = Script.ST.StateMachine;

namespace Script.Actor
{
    public class Actor : IEventWatcher
    {
        private readonly StateMachine _sm;

        private Vector3 _position = Vector3.zero;
        private Vector3 _direction = Vector3.zero;

        private STNode _latestNode;
        
        public Actor()
        {
            _sm = new StateMachine();

            var idleNode = new IdleNode(this);
            var moveNode = new MoveNode(this);
            var jumpNode = new JumpNode(this);
            
            idleNode.RegisterChangeAbleNodeList(moveNode);
            moveNode.RegisterChangeAbleNodeList(jumpNode);
            jumpNode.RegisterChangeAbleNodeList(moveNode);
            
            _sm.Init(idleNode);
        }

        public void FixedUpdate()
        {
            if (_sm != null)
            {
                _sm.UpdateNodes();
                _latestNode = _sm.CurrentNode();
            }
        }

        public void SetDir(Vector3 dir)
        {
            _direction = dir;
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
        }

        public void Excute(IEvent ev)
        {
            switch (ev.EventStr)
            {
                case "Jump":
                    _sm.ActiveNode(STNode.NodeType.Jump);
                    break;
                case "Move":
                    _sm.ActiveNode(STNode.NodeType.Move);
                    break;
            }
        }
    }
}