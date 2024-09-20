using System.Collections.Generic;

namespace Script.EventHandler
{
    public class EventManager
    {
        private Queue<IEvent> _eventQueue = new Queue<IEvent>();
        private List<IEventWatcher> _watchers = new List<IEventWatcher>();
        
        
        public void Enqueue(IEvent ev)
        {
            _eventQueue.Enqueue(ev);
        }

        public void Update()
        {
            while (_eventQueue.Count > 0)
            {
                var ev = _eventQueue.Dequeue();
                foreach (var watcher in _watchers)
                {
                    watcher.Excute(ev);
                }
            }
        }
    }
}