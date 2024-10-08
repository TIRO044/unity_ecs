using UnityEngine;

namespace Script.EventHandler
{
    public class MoveEvent : IEvent
    {
        public EventTag.Tag EventTarget { get; } = EventTag.Tag.PlayerMove;
        public string EventStr { get; } = "Move";

        public readonly Vector3 _vector3; 
        
        public MoveEvent(Vector3 vector)
        {
            _vector3 = vector;
        }
    }
}