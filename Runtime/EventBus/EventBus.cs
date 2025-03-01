namespace Framework.Eventbus
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class EventBus<T> where T : IMessage
    {
        static readonly HashSet<IMessageBinding<T>> bindings = new HashSet<IMessageBinding<T>>();

        public static void Register(MessageBinding<T> binding) => bindings.Add(binding);
        public static void Deregister(MessageBinding<T> binding) => bindings.Remove(binding);

        public static void Publish(T @event)
        {
            var snapshot = new HashSet<IMessageBinding<T>>(bindings);

            foreach (var binding in snapshot)
            {
                if (bindings.Contains(binding))
                {
                    binding.OnEvent.Invoke(@event);
                    binding.OnEventNoArgs.Invoke();
                }
            }
        }

        static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            bindings.Clear();
        }
    }
}