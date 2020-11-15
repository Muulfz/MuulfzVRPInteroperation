namespace MuulfzInteroperation.Core
{
    using System;

    public interface IFivemLibrary
    {
        string ResourceName();
        void TriggerEvent(string eventName, params object[] args);
        void Add(string key, Delegate value);
        void Remove(string key, Delegate value);
    }
}