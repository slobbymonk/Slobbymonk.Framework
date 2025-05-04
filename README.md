<h2><strong>Slobbymonk.Framework</strong> is a code framework I use for my games and projects.</h2>

<h3>Download instructions: To add this package to your unity project.</h3>
1. Download the package as a zip by pressing the button in the image

 ![image](https://github.com/user-attachments/assets/865f21e1-bf50-4334-ae9a-6f7c78cfebd0)

2. Go to package manager in unity and go to the + icon, where you click add from disk.

![image](https://github.com/user-attachments/assets/df8f591d-c46f-4c84-89cd-71be96d41673)

3. The package should be installed.

Below are the current features it provides:</p>

<h2>Features</h2>

<h3>✅ DosBehaviour</h3>
<p>An extension of Unity's default <code>MonoBehaviour</code>. It includes additional functionality used across the framework, such as automated message handling registration.</p>

<hr>

<h3>✅ EventBus</h3>
<p>A centralized system for sending and receiving decoupled messages between objects. Instead of directly referencing other classes, you can send messages through the event bus.</p>

<ul>
  <li><strong>Requires</strong> inheritance from <code>DosBehaviour</code> to handle automatic registration and deregistration of message handlers.</li>
  <li>To add a new message:
    <ol>
      <li>Define it in <code>Messages.cs</code>.</li>
      <li>Inherit from <code>IMessageHandler&lt;MessageType&gt;</code>.</li>
      <li>Implement the corresponding method.<br>
        ⚠️ <strong>Don't rename the method</strong> — <code>DosBehaviour</code> uses reflection to find it by name.<br>
        ✅ Naming convention: method must end with <code>Message</code>.
      </li>
    </ol>
  </li>
</ul>

<hr>

<h2>Example: Sending and Receiving an EventBus Message</h2>

<h3>📨 Sender</h3>

```csharp
public class NPCManager : DosBehaviour
{
    public void HandleNPCWasHit(string npcName)
    {
        Debug.Log("Oh nooo, an NPC was hit.");
        _messageHandler.Publish(new NPCHitMessage(npcName));
    }
}
<h3>🧾 Message</h3>
public class NPCHitMessage : IMessage
{
    public string NPCName;

    public NPCHitMessage(string npcName)
    {
        NPCName = npcName;
    }
}
<h3>📬 Receiver</h3>
public class NPCDeathHandler : DosBehaviour, IMessageHandler<NPCHitMessage>
{
    public void Handler(NPCHitMessage message)
    {
        Debug.Log($"Hahaha yeah {message.NPCName} just died.");
    }
}
