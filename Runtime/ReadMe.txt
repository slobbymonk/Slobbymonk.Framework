This is a framework I will use for my projects, these are the features it has currently:

DosBehaviour, an extension of the default Monobehaviour.

EventBus, allows me to send and receive decoupled messages using a centralised event bus instead of referencing other classes. Has to inherit from DosBehaviour since that handles automatically Registering and Deregistering receivers.
To add an event, or message as I call it, you just go to Messages.cs and add a message, you then inherit from IMessageHandler<Messagetype> and implement the prompted function. Don't change the function's name, this is used by DosBehaviour to find it. The naming convention is that it has to end with Message.