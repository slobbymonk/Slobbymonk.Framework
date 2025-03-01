using EventMessageSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DosBehaviour : MonoBehaviour
{
    private readonly Dictionary<Type, object> messageBindings = new();

    protected virtual void OnEnable()
    {
        RegisterHandlers();
    }

    protected virtual void OnDisable()
    {
        DeregisterHandlers();
    }

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
        foreach (var kvp in messageBindings)
        {
            Type messageType = kvp.Key;
            object bindingInstance = kvp.Value;

            var deregisterMethod = typeof(EventBus<>).MakeGenericType(messageType).GetMethod("Deregister");
            deregisterMethod?.Invoke(null, new object[] { bindingInstance });
        }

        messageBindings.Clear();
    }
}
