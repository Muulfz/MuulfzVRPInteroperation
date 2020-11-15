namespace MuulfzInteroperation.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CitizenFX.Core;
    using CitizenFX.Core.Native;
    using Core;

    public class FivemServerLibrary : BaseScript, IFivemLibrary
    {
        public FivemServerLibrary()
        {
            Proxy.FivemApi = this;
            Tunnel.FivemApi = this;
        }

        public string ResourceName()
        {
            return API.GetCurrentResourceName();
        }

        public new void TriggerEvent(string eventName, params object[] args)
        {
            BaseScript.TriggerEvent(eventName, args);
        }

        public void Add(string key, Delegate value)
        {
            EventHandlers[key] += value;
        }

        public void Remove(string key, Delegate value)
        {
            EventHandlers[key] -= value;
        }

        public void TriggerRemoteEvent(string eventName, string targetId, params object[] args)
        {
            Debug.WriteLine("Trigger Remote Event");
            if (!string.IsNullOrEmpty(targetId))
            {
                List<Player> players = Players.ToList();

                Player player = players.FirstOrDefault(e => e.Handle == targetId);

                if (player != null)
                {
                    Debug.WriteLine("Player Found");

                    player.TriggerEvent(eventName, args);
                }
                else
                {
                    Debug.WriteLine("Player Not Found");
                }
                return;
            }

            TriggerClientEvent(eventName, args);
        }
    }
}