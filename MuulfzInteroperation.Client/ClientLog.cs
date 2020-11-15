namespace MuulfzInteroperation.Client
{
    using System;
    using CitizenFX.Core;
    using CitizenFX.Core.Native;
    using Core;

    public class ClientLog : IInteroperatingLog
    {
        public string Prefix { get; set; } = "MuulfzInteroperationClient";
        public string ResourceName { get; set; } = API.GetCurrentResourceName();

        public void Log(string str)
        {
            Debug.WriteLine($"[{Prefix}] Resource: {ResourceName} | Log: {str}");
        }

        public void Log(string str, object o)
        {
            Debug.WriteLine($"[{Prefix}] Resource: {ResourceName} | Log: {o}");
        }

        public void Error(string str)
        {
            Debug.WriteLine($"[{Prefix}] Resource: {ResourceName} | Error: {str}");
        }
    }
}