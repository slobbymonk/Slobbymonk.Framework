using Framework.Eventbus;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework
{
    public abstract class RootBehaviour : MonoBehaviour
    {
        private readonly Dictionary<Type, object> messageBindings = new();
        protected virtual void Awake()
        {
            RegisterHandlers();
        }

        protected virtual void OnDestroy()
        {
            DeregisterHandlers();
        }

        protected virtual void Update() { }

        private void RegisterHandlers()
        {
            var interfaces = GetType()
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessageHandler<>));

            foreach (var interfaceType in interfaces)
            {
                Type messageType = interfaceType.GetGenericArguments()[0];
                var method = interfaceType.GetMethod("Handle");

                if (method != null)
                {
                    var bindingType = typeof(MessageBinding<>).MakeGenericType(messageType);
                    var handlerDelegate = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(messageType), this, method);

                    var bindingInstance = Activator.CreateInstance(bindingType, handlerDelegate);
                    messageBindings[messageType] = bindingInstance;

                    var registerMethod = typeof(EventBus<>).MakeGenericType(messageType).GetMethod("Register");
                    registerMethod?.Invoke(null, new object[] { bindingInstance });
                }
            }
        }

        private void DeregisterHandlers()
        {
            foreach (var messageBinding in messageBindings)
            {

                Type messageType = messageBinding.Key;

                if (!messageBindings.TryGetValue(messageType, out var bindingInstance))
                    return;

                var deregisterMethod = typeof(EventBus<>).MakeGenericType(messageType).GetMethod("Deregister");
                deregisterMethod?.Invoke(null, new object[] { bindingInstance });
            }

            messageBindings.Clear();
        }
    }
}
