namespace EventMessageSystem
{
    /// <summary>
    /// Defines message types that can be broadcasted.
    /// </summary>

    // Marker interface to identify valid messages for the event bus.
    public interface IMessage { }

    public class Template : IMessage
    {
        
    }
    public class HumanGotRunOverMessage : IMessage { }
}