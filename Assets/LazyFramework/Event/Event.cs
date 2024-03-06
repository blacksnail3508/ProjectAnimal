
using System;

namespace LazyFramework
{
    public static class Event<T> where T : IEvent
    {
        static Action<T> s_Action = delegate { };
        public static void Subscribe(Action<T> listener)
        {
            s_Action+=listener;
        }
        public static void Unsubscribe(Action<T> listener)
        {
            s_Action-=listener;
        }
        public static void Post(T @event)
        {
            s_Action.Invoke(@event);
        }
    }
}
