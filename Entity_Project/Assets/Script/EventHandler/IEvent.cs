namespace Script.EventHandler
{
    public interface IEvent
    {
        EventTag.Tag EventTarget { get; }
        string EventStr { get; }
    }
}