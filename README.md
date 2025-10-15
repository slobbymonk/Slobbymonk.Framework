<h2><strong>Slobbymonk.Framework</strong> is a code framework I use for my games and projects.</h2>

<h3>Download instructions: To add this package to your Unity project.</h3>

1. Download the package as a zip by pressing the button in the image below:

   ![Download Zip](https://github.com/user-attachments/assets/865f21e1-bf50-4334-ae9a-6f7c78cfebd0)

2. Go to the Unity Package Manager and click the <code>+</code> icon, then select <strong>Add package from disk</strong>:

   ![Add From Disk](https://github.com/user-attachments/assets/df8f591d-c46f-4c84-89cd-71be96d41673)

3. The package should now be installed in your project.

<p>Below are the current features it provides:</p>

<h2>Features</h2>

<h3>DosBehaviour</h3>
<p>An extension of Unity's default <code>MonoBehaviour</code>. It includes additional functionality used across the framework, such as automated message handling registration.</p>

<hr>

<h3>EventBus</h3>
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
```
<h3>🧾 Message</h3>

```csharp
public class NPCHitMessage : IMessage
{
    public string NPCName;

    public NPCHitMessage(string npcName)
    {
        NPCName = npcName;
    }
}
```
<h3>📬 Receiver</h3>

```csharp
public class NPCDeathHandler : DosBehaviour, IMessageHandler<NPCHitMessage>
{
    public void Handler(NPCHitMessage message)
    {
        Debug.Log($"Hahaha yeah {message.NPCName} just died.");
    }
}
```

<h2>Custom Debugger</h2>

<p>You can adjust the importance filter for debug messages by going to tools/Debugger/Set Log Level/[The Level you want to set it to].</p>
<p>You can use the custom debugger instead of the built in one by calling DebugMessagesManager instead of debug.</p>
<h3>Supported Functionality</h3>
<ul>
  <li>Choose between 6 importance types: Verbose, Info, Normal, Warning, Error, Critical. Each with their own colour.</li>
  <li>Choose a custom colour if you want.</li>
  <li>Automatically write all debug messages to a txt file when playing outside of the editor, like on a build.</li>
</ul>

<h2>Helper classes</h2>

<p> This package also includes a bunch of helpful extension methods.</p>
