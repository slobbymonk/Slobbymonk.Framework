namespace Framework.Eventbus
{
    using UnityEngine;

    public interface IMessageHandler<T> where T : IMessage
    {
        void Handle(T message);
    }
    public abstract class MessageHandlerBase<T> : MonoBehaviour, IMessageHandler<T> where T : IMessage
    {
        private MessageBinding<T> messageBinding;

        protected virtual void Awake()
        {
            messageBinding = new MessageBinding<T>(Handle);
        }

        protected virtual void OnEnable()
        {
            EventBus<T>.Register(messageBinding);
        }

        protected virtual void OnDisable()
        {
            EventBus<T>.Deregister(messageBinding);
        }

        public abstract void Handle(T message);
    }
}