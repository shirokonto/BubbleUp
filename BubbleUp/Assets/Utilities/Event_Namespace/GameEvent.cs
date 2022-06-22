using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Event_Namespace
{
    [CreateAssetMenu(fileName = "new GameEvent", menuName = "Utils/Event")]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
            //Debug.Log("<color=#2A5BA7><b> " + name + " was raised.</b></color>");
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
}
