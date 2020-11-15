namespace MuulfzInteroperation.Client
{
    using System;
    using CitizenFX.Core;
    using CitizenFX.Core.Native;
    using Core;

    public class FivemClientLibrary : BaseScript, IFivemLibrary
    {

        public FivemClientLibrary()
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
            TriggerServerEvent(eventName, args);
        }

    }
}