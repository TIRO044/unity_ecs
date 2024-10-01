using System.Collections.Generic;

namespace Script.EventHandler
{
    public static class EventHandler
    {
        private static Queue<IEvent> _eventQueue = new();
        private static Dictionary<EventTag.Tag, HashSet<IEventWatcher>> _handler = new();

        public static void RegisterEventHandle(EventTag.Tag eventTag, IEventWatcher watcher)
        {
            if (_handler.TryGetValue(i, out var watchers) == false)
            {
                watchers = new HashSet<IEventWatcher>();
                _handler.Add(i, watchers);
            }

            watchers.Add(watcher);
        }

        public static void UnRegisterWatcher(int i)
        {
            if (_handler.ContainsKey(i))
            {
                _handler.Remove(i);
            }
        }

        public static void Enqueue(IEvent eEvent)
        {
            _eventQueue.Enqueue(eEvent);
        }

        public static void UpdateEvent()
        {
            while (_eventQueue.Count > 0)
            {
                var targetEvent = _eventQueue.Dequeue();

                if (_handler.TryGetValue(targetEvent.EventTarget, out var watchers) == false)
                    continue;

                foreach (var t in watchers)
                {
                    t.Excute(targetEvent);
                }
            }
        }
    }
}