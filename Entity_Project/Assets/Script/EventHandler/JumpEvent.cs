using UnityEngine;

namespace Script.EventHandler
{
    public class JumpEvent : IEvent
    {
        public string EventStr { get; } = "Jump";
        
        public readonly Vector3 _vector3; 
        
        public JumpEvent(Vector3 vector)
        {
            _vector3 = vector;
        }
    }
}