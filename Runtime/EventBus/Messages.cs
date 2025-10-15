using UnityEngine;

namespace Framework.Eventbus
{
    /// <summary>
    /// Defines message types that can be broadcasted.
    /// </summary>

    // Marker interface to identify valid messages for the event bus.
    public interface IMessage { }

    public class Template : IMessage
    {

    }
    public class HumanGotRunOverMessage : IMessage
    {
        public Transform Human { get; set; }
        public HumanGotRunOverMessage(Transform human)
        {
            Human = human;
        }
    }
    public class PlayerRespawnMessage : IMessage { }
}