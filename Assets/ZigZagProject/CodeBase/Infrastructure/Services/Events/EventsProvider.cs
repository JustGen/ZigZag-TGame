using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Events
{
    public abstract class EventsProvider
    {
        public static Action OnLose;
        public static Action OnTap;
        public static Action OnTakeStone;

        public static void InvokeEvent(CodeBase.Events eventName)
        {
            switch (eventName)
            {
                case CodeBase.Events.OnTakeStone:
                    OnTakeStone?.Invoke();
                    Debug.Log("Custom event: Take stone!");
                    break;
                case CodeBase.Events.OnLose:
                    OnLose?.Invoke();
                    Debug.Log("Custom event: Game Lose!");
                    break;
                case CodeBase.Events.OnTap:
                    OnTap?.Invoke();
                    Debug.Log("Custom event: User Tap!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventName), eventName, null);
            }
        }
    }
}
