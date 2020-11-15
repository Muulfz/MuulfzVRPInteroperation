namespace MuulfzInteroperation.Client
{
    using System;
    using CitizenFX.Core;
    using CitizenFX.Core.Native;
    using Core;

    public class FivemServerLibrary : BaseScript, IFivemLibrary
    {
        public static FivemServerLibrary Instance { get; private set; }

        public FivemServerLibrary()
        {
            Instance = this;
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
    }
}