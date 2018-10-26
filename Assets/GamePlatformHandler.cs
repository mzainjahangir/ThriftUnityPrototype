using System;
using UnityEngine;


class GamePlatformHandler : GamePlatform.Iface
{
    public static event Action StartSpin;

    protected virtual void OnStartSpin()
    {
        var handler = StartSpin;
        if (handler != null) handler();
    }
    public void SendMessageFromPlatform()
    {
        Debug.Log("Called from Platform: SendMessageFromPlatform");
        OnStartSpin();
    }
}