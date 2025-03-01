## [1.0.0] - 2025-03-01
### First Release
- Introduces DosBehaviour, which is an extension of monobehaviour, allowing for more functionality.
- Introduces an event bus, uses DosBehaviour to automatically register and deregister messages (global events) without the user having to worry   
  about it. All the user has to do is use DosBehaviour and implement the IMessageHandler<GenericMessage> in order to receive the GenericMessage.