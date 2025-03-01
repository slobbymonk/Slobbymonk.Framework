using EventMessageSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testtt : MonoBehaviour, IMessageHandler<HumanGotRunOverMessage>
{
    public void Handle(HumanGotRunOverMessage message)
    {
        Debug.Log("It worked!");
    }
}