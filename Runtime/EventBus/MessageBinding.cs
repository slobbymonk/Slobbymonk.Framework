namespace Framework.Eventbus
{
    using System;

    public interface IMessageBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }

    public class MessageBinding<T> : IMessageBinding<T> where T : IMessage
    {
        Action<T> onMessage = _ => { };
        Action onMessageNoArgs = () => { };

        Action<T> IMessageBinding<T>.OnEvent
        {
            get => onMessage;
            set => onMessage = value;
        }

        Action IMessageBinding<T>.OnEventNoArgs
        {
            get => onMessageNoArgs;
            set => onMessageNoArgs = value;
        }

        public MessageBinding(Action<T> onMessage) => this.onMessage = onMessage;
        public MessageBinding(Action ononMessageNoArgs) => this.onMessageNoArgs = ononMessageNoArgs;

        public void Add(Action onMessage) => onMessageNoArgs += onMessage;
        public void Remove(Action onMessage) => onMessageNoArgs -= onMessage;

        public void Add(Action<T> onMessage) => this.onMessage += onMessage;
        public void Remove(Action<T> onMessage) => this.onMessage -= onMessage;
    }
}